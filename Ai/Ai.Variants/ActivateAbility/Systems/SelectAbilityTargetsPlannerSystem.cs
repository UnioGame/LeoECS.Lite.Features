namespace Game.Ecs.GameAi.ActivateAbility
{
    using System;
    using System.Runtime.CompilerServices;
    using Ability.Aspects;
    using Ability.SubFeatures.Target.Tools;
    using Ability.Tools;
    using AI.Components;
    using Code.Ai.ActivateAbility;
    using Code.Ai.ActivateAbility.Aspects;
    using Code.GameLayers.Relationship;
    using Components;
    using Core.Components;
    using Core.Death.Components;
    using GameLayers.Layer.Components;
    using Leopotam.EcsLite;
    using Selection;
    using TargetSelection;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.Mathematics;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class SelectAbilityTargetsPlannerSystem : IEcsRunSystem , IEcsInitSystem
    {
        private AbilityTools _abilityTools;
        private AbilityTargetTools _targetTools;
        
        private EcsWorld _world;
        private EcsFilter _filter;
        private AbilityAiActionAspect _targetAspect;
        private AbilityOwnerAspect _abilityOwnerAspect;
        private TargetSelectionSystem _targetSelection;
        
        private EcsPool<AbilityActionActiveTargetComponent> _activeTargetPool;
        private EcsPool<AbilityAiActionTargetComponent> _targetPool;
        private int[] _resultSelection = new int[TargetSelectionData.MaxTargets];

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _targetSelection = _world.GetGlobal<TargetSelectionSystem>();
            _abilityTools = _world.GetGlobal<AbilityTools>();
            _targetTools = _world.GetGlobal<AbilityTargetTools>();
            
            _filter = _world
                .Filter<AiAgentComponent>()
                .Inc<AbilityByDefaultComponent>()
                .Inc<TransformPositionComponent>()
                .Inc<LayerIdComponent>()
                .Inc<ActivateAbilityPlannerComponent>()
                .Exc<AbilityAiActionTargetComponent>()
                .Exc<DisabledComponent>()
                .Exc<PrepareToDeathComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                if(_abilityTools.GetNonDefaultAbilityInUse(entity) >= 0) continue;
                
                ref var defaultAbilityComponent = ref _targetAspect.DefaultAbility.Get(entity);
                var abilityFilters = defaultAbilityComponent.FilterData;
                var targetFound = false;

                for (var index = 0; index < abilityFilters.Length; index++)
                {
                    if(targetFound) break;
                    
                    ref var abilityFilter = ref defaultAbilityComponent.FilterData[index];
                    var slot = abilityFilter.Slot;

                    if (_abilityOwnerAspect.ApplyAbilityBySlot.Has(entity))
                    {
                        ref var slotRequest = ref _abilityOwnerAspect.ApplyAbilityBySlot.Get(entity);
                        slot = slotRequest.AbilitySlot;
                    }

                    var abilityEntity = _abilityTools.TryGetAbility(entity, slot);
                    if (abilityEntity < 0) continue;
                    
                    var abilityTargetEntity = SelectAbilityTarget(entity, ref abilityEntity, ref abilityFilter);
                    
                    if (abilityTargetEntity < 0)
                    {
                        _targetTools.ClearAbilityTargets(abilityEntity);
                        continue;
                    }

                    var packedTarget = _world.PackEntity(abilityTargetEntity);
   
                    _targetTools.SetAbilityTarget(abilityEntity,packedTarget,slot);
                    
                    var cooldownPassed = _abilityTools.IsAbilityCooldownPassed(abilityEntity);
                    
                    //проверяем кулдаун абилки, если он не прошел - игнорируем
                    if (targetFound || !cooldownPassed) continue;
                    
                    ref var activeTargetComponent = ref _activeTargetPool.GetOrAddComponent(abilityEntity);
                    activeTargetComponent.Value = packedTarget;
                    
                    targetFound = true;
                    ref var targetComponent = ref _targetPool.Add(entity);
                        
                    targetComponent.Ability = _world.PackEntity(abilityEntity);
                    targetComponent.AbilityCellId = slot;
                    targetComponent.AbilityTarget = packedTarget;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int SelectAbilityTarget(int entity, ref int ability, ref AbilityFilter filter)
        {
            ref var activeTargetComponent = ref _targetAspect.ActiveTarget.GetOrAddComponent(ability);
            ref var radiusComponent = ref _targetAspect.Radius.Get(ability);
            ref var filterComponent = ref _targetAspect.Relationship.Get(ability);
            ref var categoryIdComponent = ref _targetAspect.Category.Get(ability);
            
            ref var gameLayerComponent = ref _targetAspect.LayerId.Get(entity);
            ref var positionComponent = ref _targetAspect.Position.Get(entity);
            
            var entityPosition = positionComponent.Position;
            
            //create relation mask by gamelayer id
            var gameLayerMask = filterComponent.Value.GetFilterMask(gameLayerComponent.Value);

            var amount = _targetSelection.SelectEntitiesInArea(
                _resultSelection,
                radiusComponent.Value,
                ref entityPosition,
                ref gameLayerMask,
                ref categoryIdComponent.Value);

            var target = TargetSelectionData.EmptyResult;
            var maxPriority = float.MinValue;
            var distanceToTarget = float.MaxValue;

            var checkActiveTarget = activeTargetComponent.
                Value.Unpack(_world, out var activeTargetEntity);

            for (var j = 0; j < amount; j++)
            {
                var selectionEntity = _resultSelection[j];
                
                ref var selectionCategoryComponent = ref _targetAspect.Category.Get(selectionEntity);
                ref var transformComponent = ref _targetAspect.Position.Get(selectionEntity);

                ref var position = ref transformComponent.Position;
                var count = filter.Priorities.Length;
                var priorities = filter.Priorities;
                var priority = 0f;

                for (var i = 0; i < count && filter.UsePriority; i++)
                {
                    var categoryValue = priorities[i];
                    var category = categoryValue.Category;
                    if ((category & selectionCategoryComponent.Value) == 0)
                        continue;

                    var mult = (count - i);
                    priority += mult;
                }
  
                if (priority < maxPriority) continue;

                var distance = math.distancesq(position, entityPosition);
                
                if (priority >= maxPriority && 
                    distance >= distanceToTarget) continue;

                distanceToTarget = distance;
                target = selectionEntity;
            }
            
            return target == TargetSelectionData.EmptyResult 
                ? TargetSelectionData.EmptyResult 
                : target;
        }
    }
}