namespace Game.Ecs.ViewControl.Systems
{
    using Components;
    using Core.Components;
    using Core.Death.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    public sealed class ProcessDestroyedOwnerViewSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<ViewInstanceComponent> _viewInstancePool;
        private EcsPool<KillRequest> _killPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<ViewInstanceComponent>()
                .Inc<OwnerDestroyedEvent>()
                .End();
            _viewInstancePool = _world.GetPool<ViewInstanceComponent>();
            _killPool = _world.GetPool<KillRequest>();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                _killPool.GetOrAddComponent(entity);
                _world.DelEntity(entity);
            }
        }
    }
}