namespace Game.Ecs.Characteristics.Cooldown.Systems
{
    using Base.Components.Events;
    using Components;
    using Leopotam.EcsLite;

    public sealed class ResetCooldownSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<BaseCooldownComponent>()
                .Inc<ResetCharacteristicsEvent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var baseCooldownPool = _world.GetPool<BaseCooldownComponent>();
            var requestPool = _world.GetPool<RecalculateCooldownSelfRequest>();

            foreach (var entity in _filter)
            {
                ref var baseCooldown = ref baseCooldownPool.Get(entity);
                baseCooldown.Modifications.Clear();
                
                if (!requestPool.Has(entity))
                    requestPool.Add(entity);
            }
        }
    }
}