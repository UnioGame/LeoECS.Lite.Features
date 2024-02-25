namespace Game.Ecs.Characteristics.Base.Systems
{
    using Components;
    using Components.Requests;
    using Game.Modules.UnioModules.UniGame.LeoEcsLite.LeoEcs.Shared.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;

    public class ResetCharacteristicsSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsFilter _requestFilter;

        private EcsPool<CharacteristicValueComponent> _characteristicPool;
        private EcsPool<ResetCharacteristicRequest> _resetCharacteristicPool;
        private EcsPool<CharacteristicDefaultValueComponent> _defaultPool;
        private EcsPool<MinValueComponent> _minPool;
        private EcsPool<MinValueComponent> _maxPool;
        private EcsPool<CharacteristicBaseValueComponent> _basePool;
        private EcsPool<ResetModificationsRequest> _resetModificationPool;
        private EcsPool<RecalculateCharacteristicSelfRequest> _recalculateCharacteristicPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _requestFilter = _world
                .Filter<ResetCharacteristicRequest>()
                .End();

            _resetCharacteristicPool = _world.GetPool<ResetCharacteristicRequest>();
            _resetModificationPool = _world.GetPool<ResetModificationsRequest>();
            _characteristicPool = _world.GetPool<CharacteristicValueComponent>();
            _defaultPool = _world.GetPool<CharacteristicDefaultValueComponent>();
            _minPool = _world.GetPool<MinValueComponent>();
            _maxPool = _world.GetPool<MinValueComponent>();
            _basePool = _world.GetPool<CharacteristicBaseValueComponent>();
            _recalculateCharacteristicPool = _world.GetPool<RecalculateCharacteristicSelfRequest>();

        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var requestEntity in _requestFilter)
            {
                ref var resetRequestComponent = ref _resetCharacteristicPool.Get(requestEntity);
                if(!resetRequestComponent.Target.Unpack(_world,out var targetEntity))
                    continue;
                
                //reset base value to default
                ref var baseValuePoolComponent = ref _basePool.Get(targetEntity);
                ref var defaultPoolComponent = ref _defaultPool.Get(targetEntity);
                ref var characteristicValueComponent = ref _characteristicPool.Get(targetEntity);
                ref var minComponent = ref _minPool.Get(targetEntity);
                ref var maxComponent = ref _maxPool.Get(targetEntity);

                maxComponent.Value = defaultPoolComponent.MaxValue;
                minComponent.Value = defaultPoolComponent.MinValue;
                characteristicValueComponent.Value = defaultPoolComponent.Value;
                baseValuePoolComponent.Value = defaultPoolComponent.BaseValue;

                var resetModificationEntity = _world.NewEntity();
                ref var resetModificationComponent = ref _resetModificationPool.Add(resetModificationEntity);
                resetModificationComponent.Characteristic = resetRequestComponent.Target;

                _recalculateCharacteristicPool.GetOrAddComponent(targetEntity);
            }
            
        }
    }
}