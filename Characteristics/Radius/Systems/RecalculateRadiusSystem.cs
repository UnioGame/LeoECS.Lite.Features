namespace Game.Ecs.Characteristics.Radius.Systems
{
    using Base.Components;
    using Component;
    using Leopotam.EcsLite;

    public sealed class RecalculateRadiusSystem : IEcsRunSystem,IEcsInitSystem
    {
        
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EcsPool<CharacteristicComponent<RadiusComponent>> _characteristicPool;
        private EcsPool<RadiusComponent> _characteristicComponentPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<CharacteristicChangedComponent<RadiusComponent>>()
                .Inc<CharacteristicComponent<RadiusComponent>>()
                .Inc<RadiusComponent>()
                .End();

            _characteristicPool = _world.GetPool<CharacteristicComponent<RadiusComponent>>();
            _characteristicComponentPool = _world.GetPool<RadiusComponent>();
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