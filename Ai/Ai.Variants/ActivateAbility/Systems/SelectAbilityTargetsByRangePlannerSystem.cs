namespace Game.Ecs.GameAi.ActivateAbility
{
    using System;
    using Ability.Common.Components;
    using Ability.SubFeatures.Target.Tools;
    using Ability.Tools;
    using AI.Components;
    using Code.Ai.ActivateAbility;
    using Code.Ai.ActivateAbility.Aspects;
    using Code.GameLayers.Relationship;
    using Components;
    using Core.Components;
    using GameLayers.Layer.Components;
    using Input.Components;
    using Leopotam.EcsLite;
    using Selection;
    using TargetSelection;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.Mathematics;
    using UnityEngine;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class SelectAbilityTargetsByRangePlannerSystem : IEcsRunSystem , IEcsInitSystem
    {
        private AbilityTools _abilityTools;
        private AbilityTargetTools _targetTools;
        private TargetSelectionSystem _targetSelection;
        
        private AbilityAiActionAspect _targetAspect;
        
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsFilter _abilityRequestFilter;
        
        private EcsPool<AbilityAiActionTargetComponent> _targetPool;

        private int[] _selection = new int[TargetSelectionData.MaxTargets];

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _targetSelection = _world.GetGlobal<TargetSelectionSystem>();
            _abilityTools = _world.GetGlobal<AbilityTools>();
            _targetTools = _world.GetGlobal<AbilityTargetTools>();
            
            _abilityRequestFilter = _world
                .Filter<ApplyAbilityBySlotSelfRequest>()
                .Inc<UserInputTargetComponent>()
                .End();
            
            _filter = _world
                .Filter<AiAgentComponent>()
                .Inc<AbilityByRangeComponent>()
                .Inc<TransformPositionComponent>()
                .Inc<LayerIdComponent>()
                .Inc<ActivateAbilityPlannerComponent>()
                .Exc<AbilityAiActionTargetComponent>()
                .Exc<PrepareToDeathComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                if(_abilityTools.GetNonDefaultAbilityInUse(entity) >= 0 ) continue;
                
                ref var radiusComponent = ref _targetAspect.ByRangeAbility.Get(entity);
                ref var rangeComponent = ref _targetAspect.ByRangeAbility.Get(entity);
                var abilityFilters = rangeComponent.FilterData;
                var targetFound = false;

                var sqrRadius = radiusComponent.Radius * radiusComponent.Radius;
                var sqrMinDistance = radiusComponent.MinDistance * radiusComponent.MinDistance;
                
                for (var index = 0; index < abilityFilters.Length; index++)
                {
                    ref var abilityFilter = ref rangeComponent.FilterData[index];
                    var abilitySlot = abilityFilter.Slot;
                    var abilityEntity = _abilityTools.GetAbilityBySlot(entity, abilitySlot);
                    
                    foreach (var abilityRequestEntity in _abilityRequestFilter)
                    {
                        if(entity != abilityRequestEntity) continue;
                        var requestAbility =_abilityTools.GetAbilityBySlot(entity, abilitySlot);
                        abilityEntity = requestAbility < 0 ? abilityEntity : requestAbility;
                        break;
                    }

                    if(abilityEntity < 0) continue;
                    
                    var abilityTargetEntity = SelectAbilityTarget(
                        entity,
                        ref abilityEntity,
                        ref abilityFilter,
                        sqrRadius,sqrMinDistance);

                    if (abilityTargetEntity == TargetSelectionData.EmptyResult)
                    {
                        _targetTools.ClearAbilityTargets(abilityEntity);
                        continue;
                    }

                    var packedTarget = _world.PackEntity(abilityTargetEntity);
                    
                    _targetTools.SetAbilityTarget(abilityEntity,packedTarget,abilitySlot);
                    
                    var cooldownPassed = _abilityTools.IsAbilityCooldownPassed(abilityEntity);
                    
                    //проверяем кулдаун абилки, если он не прошел - игнорируем
                    if (targetFound || !cooldownPassed) continue;
                    
                    ref var activeTargetComponent = ref _targetAspect.ActiveTarget.GetOrAddComponent(abilityEntity);
                    activeTargetComponent.Value = packedTarget;
                    
                    targetFound = true;
                    ref var targetComponent = ref _targetPool.Add(entity);
                        
                    targetComponent.Ability = _world.PackEntity(abilityEntity);
                    targetComponent.AbilityCellId = abilitySlot;
                    targetComponent.AbilityTarget = packedTarget;
                }
            }
        }

        private int SelectAbilityTarget(int entity, ref int ability,
            ref AbilityFilter filter, 
            float radius ,
            float minDistance)
        {
            ref var gameLayerComponent = ref _targetAspect.LayerId.Get(entity);
            ref var transformPositionComponent = ref _targetAspect.Position.Get(entity);
            
            ref var filterComponent = ref _targetAspect.Relationship.Get(ability);
            ref var categoryIdComponent = ref _targetAspect.Category.Get(ability);
            var entityPosition = transformPositionComponent.Position;
            
            //create relation mask by gamelayer id
            var gameLayerMask = filterComponent.Value.GetFilterMask(gameLayerComponent.Value);

            var amount = _targetSelection.SelectEntitiesInArea(
                _selection,
                radius,
                ref entityPosition,
                ref gameLayerMask,
                ref categoryIdComponent.Value);

            var target = TargetSelectionData.EmptyResult;
            var maxPriority = float.MinValue;
            var distanceToTarget = float.MaxValue;
            
            for (var j = 0; j < amount; j++)
            {
                var selectionEntity = _selection[j];

                ref var selectionCategoryComponent = ref _targetAspect.Category.Get(selectionEntity);
                ref var positionComponent = ref _targetAspect.Position.Get(selectionEntity);

                ref var position = ref positionComponent.Position;
                var distance = math.distancesq(position, entityPosition);
                
                if(distance < minDistance) continue;
                
                var priority = 0f;
                var count = filter.Priorities.Length;
                var categories = filter.Priorities;
                
                for (var i = 0; i < count && filter.UsePriority; i++)
                {
                    var categoryValue = categories[i];
                    var category = categoryValue.Category;
                    
                    if ((category & selectionCategoryComponent.Value) == 0)
                        continue;

                    var mult = (count - i);
                    priority += mult;
                }

                if (priority < maxPriority) continue;
                
                if (Mathf.Approximately(priority, maxPriority) && 
                    distance > distanceToTarget - 0.1f)
                    continue;

                distanceToTarget = distance;
                target = selectionEntity;
            }
            
            if (target == TargetSelectionData.EmptyResult)
                return TargetSelectionData.EmptyResult;

            return target;
        }
    }
}