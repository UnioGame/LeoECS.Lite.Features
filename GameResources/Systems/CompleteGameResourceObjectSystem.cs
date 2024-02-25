namespace Game.Ecs.GameResources.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcsLite.LeoEcs.Shared.Components;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class CompleteGameResourceObjectSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private GameResourceAspect _resourceAspect;
        private EcsPool<GameResourceSpawnCompleteEvent> _spawnEventPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<UnityObjectComponent>()
                .Inc<GameSpawnedResourceComponent>()
                .Exc<GameSpawnCompleteComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var resourceComponent = ref _resourceAspect.Resource.Get(entity);
                ref var objectComponent = ref _resourceAspect.Object.Get(entity);
                ref var spawnedResourceComponent = ref _resourceAspect.SpawnedResource.Get(entity);
                ref var completeComponent = ref _resourceAspect.Complete.Add(entity);

                var eventEntity = _world.NewEntity();
                ref var eventComponent = ref _spawnEventPool.Add(eventEntity);
                
                eventComponent.SpawnedEntity = _world.PackEntity(entity);
                eventComponent.Resource = objectComponent.Value;
                eventComponent.Source = spawnedResourceComponent.Source;
                eventComponent.ResourceId = resourceComponent.Value;
            }
        }

    }
}