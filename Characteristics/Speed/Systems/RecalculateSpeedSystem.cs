namespace Game.Ecs.Characteristics.Speed.Systems
{
    using Base.Components;
    using Components;
    using Leopotam.EcsLite;

    public sealed class RecalculateSpeedSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EcsPool<CharacteristicComponent<SpeedComponent>> _characteristicPool;
        private EcsPool<SpeedComponent> _characteristicComponentPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<CharacteristicChangedComponent<SpeedComponent>>()
                .Inc<CharacteristicComponent<SpeedComponent>>()
                .Inc<SpeedComponent>()
                .End();

            _characteristicPool = _world.GetPool<CharacteristicComponent<SpeedComponent>>();
            _characteristicComponentPool = _world.GetPool<SpeedComponent>();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var characteristicComponent = ref _characteristicPool.Get(entity);
                ref var characteristicValueComponent = ref _characteristicComponentPool.Get(entity);
                characteristicValueComponent.Value = characteristicComponent.Value;
            }
        }
    }
}