namespace Game.Ecs.Characteristics.Health.Systems
{
    using Components;
    using Game.Ecs.Characteristics.Base.Components;
    using Leopotam.EcsLite;

    public sealed class ProcessHealthChangedSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsFilter _filterDestinations;
        private EcsWorld _world;
        
        private EcsPool<CharacteristicComponent<HealthComponent>> _characteristicPool;
        private EcsPool<HealthComponent> _healthPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<CharacteristicChangedComponent<HealthComponent>>()
                .Inc<CharacteristicComponent<HealthComponent>>()
                .Inc<HealthComponent>()
                .End();

            _characteristicPool = _world.GetPool<CharacteristicComponent<HealthComponent>>();
            _healthPool = _world.GetPool<HealthComponent>();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var characteristicComponent = ref _characteristicPool.Get(entity);
                ref var healthComponent = ref _healthPool.Get(entity);
                healthComponent.Health = characteristicComponent.Value;
                healthComponent.MaxHealth = characteristicComponent.MaxValue;
            }
        }
    }
}