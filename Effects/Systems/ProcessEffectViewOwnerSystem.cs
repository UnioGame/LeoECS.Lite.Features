namespace Game.Ecs.Effects.Systems
{
    using Components;
    using Core.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;

    public sealed class ProcessEffectViewOwnerSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<EffectViewComponent>()
                .Inc<OwnerDestroyedEvent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var destroyRequestPool = _world.GetPool<DestroyEffectViewSelfRequest>();

            foreach (var entity in _filter)
            {
                destroyRequestPool.TryAdd(entity);
            }
        }
    }
}