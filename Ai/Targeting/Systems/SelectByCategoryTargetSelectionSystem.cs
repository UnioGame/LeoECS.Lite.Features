namespace Game.Ecs.Ai.Targeting.Systems
{
    using System;
    using Aspects;
    using Leopotam.EcsLite;
    using Components;
    using TargetSelection.Aspects;
    using Game.Ecs.AI.Components;
    using GameLayers.Layer.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class SelectByCategoryTargetSelectionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;

        private TargetingAspect _targetingAspect;
        private TargetSelectionAspect _targetSelectionAspect;
        
        private EcsPool<LayerIdComponent> _layerPool;
        private EcsPool<AiSensorComponent> _sensorPool;

        private EcsFilter _targetSelectionFilter;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _targetSelectionFilter = _world
                .Filter<SelectByCategoryComponent>()
                .Inc<AiAgentComponent>()
                .Inc<AiSensorComponent>()
                .Inc<LayerIdComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var targetSelectionEntity in _targetSelectionFilter)
            {
                ref var selectComponent = ref _targetingAspect.SelectByCategory.Get(targetSelectionEntity);
                ref var sensorComponent = ref _sensorPool.Get(targetSelectionEntity);
                ref var layerComponent = ref _layerPool.Get(targetSelectionEntity);
                ref var request = ref _targetSelectionAspect.TargetSelectionRequest.Add(targetSelectionEntity);
                
                request.SourceLayer = layerComponent.Value;
                request.Relationship = selectComponent.Relationship;
                request.Category = selectComponent.CategoryId;
                request.Radius = sensorComponent.Range;
            }
        }
    }
}