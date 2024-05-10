namespace Game.Ecs.GameAi.Targeting.Systems
{
    using System;
    using Aspects;
    using Leopotam.EcsLite;
    using Components;
    using TargetSelection.Aspects;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using Game.Ecs.AI.Components;
    using Game.Ecs.GameLayers.Layer.Components;
    using Game.Ecs.Core.Components;
    using global::Characteristics.Radius.Abstract;

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
    public class SelectByCategoryTargetSelectionSystem<T> : IEcsInitSystem, IEcsRunSystem
        where T : struct, IRadius
    {
        private EcsWorld _world;

        private TargetingAspect _targetingAspect;
        private TargetSelectionAspect _targetSelectionAspect;

        private EcsPool<OwnerComponent> _ownerPool;
        private EcsPool<LayerIdComponent> _layerPool;
        private EcsPool<T> _radiusPool;

        private EcsFilter _targetSelectionFilter;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _targetSelectionFilter = _world
                .Filter<SelectByCategoryComponent>()
                .Inc<OwnerComponent>()
                .Inc<T>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var targetSelectionEntity in _targetSelectionFilter)
            {
                ref var ownerComponent = ref _ownerPool.Get(targetSelectionEntity);
                if (!ownerComponent.Value.Unpack(_world, out int ownerEntity))
                {
                    continue;
                }

                ref var selectComponent = ref _targetingAspect.SelectByCategory.Get(targetSelectionEntity);
                ref var radiusComponent = ref _radiusPool.Get(ownerEntity);
                ref var layerComponent = ref _layerPool.Get(ownerEntity);
                
                ref var request = ref _targetSelectionAspect.TargetSelectionRequest.Add(targetSelectionEntity);
                request.SourceLayer = layerComponent.Value;
                request.Relationship = selectComponent.Relationship;
                request.Category = selectComponent.CategoryId;
                request.Radius = radiusComponent.Radius;
                request.ResultHash = selectComponent.ActionId;
            }
        }
    }
}