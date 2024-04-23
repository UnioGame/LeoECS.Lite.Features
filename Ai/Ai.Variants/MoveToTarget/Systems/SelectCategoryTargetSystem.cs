namespace Game.Ecs.GameAi.MoveToTarget.Systems
{
    using System;
    using Ability.Tools;
    using Characteristics.Radius.Component;
    using Code.GameLayers.Relationship;
    using Components;
    using Core.Death.Components;
    using Data;
    using Game.Ecs.GameLayers.Layer.Components;
    using Selection;
    using Leopotam.EcsLite;
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
    public class SelectCategoryTargetSystem : IEcsRunSystem,IEcsInitSystem
    {
        private AbilityTools _abilityTools;
        private EcsFilter _filter;
        private EcsWorld _world;
        private TargetSelectionSystem _targetSelection;
        
        private EcsPool<MoveByCategoryComponent> _moveToTargetPlannerPool;
        private EcsPool<MoveToGoalComponent> _goalPool;
        private EcsPool<RadiusComponent> _radiusPool;
        private EcsPool<TransformPositionComponent> _positionPool;
        private EcsPool<LayerIdComponent> _layerPool;

        private int[] _selection = new int[TargetSelectionData.MaxTargets];
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _abilityTools = _world.GetGlobal<AbilityTools>();
            _targetSelection = _world.GetGlobal<TargetSelectionSystem>();
            
            _filter = _world
                .Filter<MoveByCategoryComponent>()
                .Inc<MoveToTargetPlannerComponent>()
                .Inc<MoveToGoalComponent>()
                .Inc<TransformPositionComponent>()
                .Inc<LayerIdComponent>()
                .Exc<DisabledComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var plannerComponent = ref _moveToTargetPlannerPool.Get(entity);

                if (!_abilityTools.TryGetInHandAbility(_world,entity, out var ability))
                    continue;

                ref var transformComponent = ref _positionPool.Get(entity);
                ref var layerComponent = ref _layerPool.Get(entity);
           
                var layer = layerComponent.Value;

                var targetEntity = TargetSelectionData.EmptyResult;
                ref var position = ref transformComponent.Position;
                
                var targetPriority = float.MinValue;
                var maxDistancePriority = plannerComponent.MaxPriorityByDistance;
                var distanceToTarget = float.MaxValue;
                var targetPosition = position;
                var minPriority = plannerComponent.MinFilteredTargetPriority;

                var sqrEffectiveDistance = plannerComponent.EffectiveDistance * plannerComponent.EffectiveDistance;
                
                foreach (var category in plannerComponent.FilterData)
                {
                    var categoryId = category.CategoryId;
                    var mask = category.Relationship.GetFilterMask(layer);
                    
                    var count = _targetSelection.SelectEntitiesInArea(
                        _selection, 
                        category.SensorDistance , 
                        ref position, 
                        ref mask,
                        ref categoryId);
                    
                    for (var i = 0; i < count; i++)
                    {
                        var selectionEntity = _selection[i];
                        var priority = minPriority + count - i;

                        ref var selectionTransformComponent = ref _positionPool.Get(selectionEntity);
                        ref var selectionPosition = ref selectionTransformComponent.Position;
                        
                        var distance = math.distancesq(selectionPosition, position);
                        
                        var distancePriority = sqrEffectiveDistance <= 0
                            ? 0
                            : math.lerp(maxDistancePriority, 0, distance / sqrEffectiveDistance);
                        
                        priority += distancePriority;

                        if (priority <= targetPriority) continue;

                        distanceToTarget = distance;
                        targetPriority = priority;
                        targetEntity = selectionEntity;
                        targetPosition = selectionPosition;
                    }
                }

                if (targetEntity == TargetSelectionData.EmptyResult)
                    continue;

                ref var abilityRadiusComponent = ref _radiusPool.Get(ability);
                var radius = abilityRadiusComponent.Value;
                var complete = distanceToTarget < radius;
                targetPriority = complete ? -1 : targetPriority;
            
                if(targetPriority < 0) continue;
                
                ref var component = ref _goalPool.Get(entity);

                var value = new MoveToGoalData()
                {
                    Complete = complete,
                    Position = targetPosition,
                    Priority = targetPriority,
                    Target = _world.PackEntity(targetEntity)
                };
            
                component.Goals.Add(value);
            }
        }
    }
}