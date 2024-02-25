namespace Game.Ecs.Characteristics.Shield.Systems
{
    using Base.Components.Events;
    using Components;
    using Leopotam.EcsLite;

    public sealed class ResetShieldSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<ShieldComponent>()
                .Inc<ResetCharacteristicsEvent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var shieldPool = _world.GetPool<ShieldComponent>();
            
            foreach (var entity in _filter)
            {
                shieldPool.Del(entity);
            }
        }
    }
}