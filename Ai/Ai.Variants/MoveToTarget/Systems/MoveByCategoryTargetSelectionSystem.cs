namespace Game.Ecs.GameAi.MoveToTarget.Systems
{
    using System;
    using Leopotam.EcsLite;
    using Components;
    using TargetSelection.Aspects;
    using Game.Ecs.TargetSelection.Components;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using Game.Ecs.AI.Components;
    using Game.Ecs.GameLayers.Layer.Components;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class MoveByCategoryTargetSelectionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private int _parentId;

        private TargetSelectionAspect _targetSelectionAspect;
        private EcsPool<MoveByCategoryComponent> _moveByCategoryPool;
        private EcsPool<LayerIdComponent> _layerComponent;
        private EcsPool<MoveToTargetComponent> _moveToTargetPool;
        private EcsPool<TransformPositionComponent> _transformPositionPool;
        private EcsPool<AiSensorComponent> _sensorPool;

        private EcsFilter _targetSelectionFilter;

        public MoveByCategoryTargetSelectionSystem(int parentId)
        {
            _parentId = parentId;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _targetSelectionFilter = _world
                .Filter<SqrRangeTargetsSelectionComponent>()
                .Inc<TransformPositionComponent>()
                .Inc<MoveByCategoryComponent>()
                .Inc<LayerIdComponent>()
                .Inc<AiSensorComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var targetSelectionEntity in _targetSelectionFilter)
            {
                ref var moveByCategoryComponent = ref _moveByCategoryPool.Get(targetSelectionEntity);
                ref var targetSelectionComponent = ref _targetSelectionAspect.TargetSelection.Get(targetSelectionEntity);
                ref var result = ref targetSelectionComponent.Results[_parentId];
                if (result.Ready && result.Count > 0)
                {
                    ref var moveToTargetComponent = ref _moveToTargetPool.GetOrAddComponent(targetSelectionEntity);
                    if (result.Values[0].Unpack(_world, out var targetEntity))
                    {
                        ref var transformPositionComponent = ref _transformPositionPool.Get(targetEntity);

                        if (moveByCategoryComponent.MaxPriorityByDistance > moveToTargetComponent.Priority)
                        {
                            moveToTargetComponent.TargetPosition = transformPositionComponent.Position;
                            moveToTargetComponent.Priority = moveByCategoryComponent.MaxPriorityByDistance;
                        }
                    }
                }

                result.Ready = false;

                ref var sensorComponent = ref _sensorPool.Get(targetSelectionEntity);
                ref var layerComponent = ref _layerComponent.Get(targetSelectionEntity);
                ref var request = ref targetSelectionComponent.Requests[_parentId];

                var filter = moveByCategoryComponent.FilterData;
                request.SourceLayer = layerComponent.Value;
                request.Relationship = filter.Relationship;
                request.Category = filter.CategoryId;
                request.Processed = false;
                request.Radius = sensorComponent.Range;
                request.Target = targetSelectionEntity.PackedEntity(_world);
            }
        }
    }
}