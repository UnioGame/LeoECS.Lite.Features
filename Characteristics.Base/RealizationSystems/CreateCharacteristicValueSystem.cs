namespace Game.Ecs.Characteristics.Base.RealizationSystems
{
    using System;
    using Components;
    using Components.Requests;
    using Game.Ecs.Core.Components;
    using Game.Modules.UnioModules.UniGame.LeoEcsLite.LeoEcs.Shared.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// create new characteristic value entity for target
    /// by request CreateCharacteristicRequest<TCharacteristic>
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class CreateCharacteristicValueSystem<TCharacteristic> 
        : IEcsInitSystem, IEcsRunSystem
        where TCharacteristic : struct
    {
        private EcsWorld _world;
        private EcsFilter _requestFilter;
        
        private EcsPool<CreateCharacteristicRequest<TCharacteristic>> _requestPool;
        private EcsPool<CharacteristicLinkComponent<TCharacteristic>> _characteristicLinkPool;
        private EcsPool<CharacteristicValueComponent> _characteristicPool;
        private EcsPool<MinValueComponent> _minValuePool;
        private EcsPool<MaxValueComponent> _maxValuePool;
        private EcsPool<CharacteristicBaseValueComponent> _baseValuePool;
        private EcsPool<OwnerComponent> _ownerPool;
        private EcsPool<CharacteristicOwnerComponent<TCharacteristic>> _characteristicsOwnerPool;
        private EcsPool<CharacteristicComponent<TCharacteristic>> _characteristicValuePool;
        private EcsPool<CharacteristicDefaultValueComponent> _defaultValuePool;
        private EcsPool<CharacteristicPreviousValueComponent> _previousValuePool;
        private EcsPool<CharacteristicChangedComponent> _changedPool;
        private EcsPool<PercentModificationsValueComponent> _modificationPercentPool;
        private EcsPool<MaxLimitModificationsValueComponent> _maxLimitModificationsPool;
        private EcsPool<BaseModificationsValueComponent> _valueModificationsPool;
        private EcsPool<CharacteristicChangedComponent<TCharacteristic>> _ownerChangedPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _requestFilter = _world
                .Filter<CreateCharacteristicRequest<TCharacteristic>>()
                .End();

            _requestPool = _world.GetPool<CreateCharacteristicRequest<TCharacteristic>>();
            
            _characteristicLinkPool = _world.GetPool<CharacteristicLinkComponent<TCharacteristic>>();
            _ownerChangedPool = _world.GetPool<CharacteristicChangedComponent<TCharacteristic>>();
            _characteristicsOwnerPool = _world.GetPool<CharacteristicOwnerComponent<TCharacteristic>>();
            
            _characteristicPool = _world.GetPool<CharacteristicValueComponent>();
            _minValuePool = _world.GetPool<MinValueComponent>();
            _maxValuePool = _world.GetPool<MaxValueComponent>();
            _baseValuePool = _world.GetPool<CharacteristicBaseValueComponent>();
            _ownerPool = _world.GetPool<OwnerComponent>();
            _characteristicValuePool = _world.GetPool<CharacteristicComponent<TCharacteristic>>();
            _changedPool = _world.GetPool<CharacteristicChangedComponent>();
            _defaultValuePool = _world.GetPool<CharacteristicDefaultValueComponent>();
            _previousValuePool = _world.GetPool<CharacteristicPreviousValueComponent>();
            _modificationPercentPool = _world.GetPool<PercentModificationsValueComponent>();
            _maxLimitModificationsPool = _world.GetPool<MaxLimitModificationsValueComponent>();
            _valueModificationsPool = _world.GetPool<BaseModificationsValueComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var request in _requestFilter)
            {
                ref var requestComponent = ref _requestPool.Get(request);

                //is owner alive?
                if (!requestComponent.Owner.Unpack(_world, out var ownerEntity))
                    continue;

                if (_characteristicLinkPool.Has(ownerEntity))
                    continue;

                _ownerChangedPool.GetOrAddComponent(ownerEntity);
                
                var characteristicEntity = _world.NewEntity();
                var characteristicsPacked = _world.PackEntity(characteristicEntity);
                
                ref var characteristicComponent = ref _characteristicPool.Add(characteristicEntity);
                ref var minComponent = ref _minValuePool.Add(characteristicEntity);
                ref var maxComponent = ref _maxValuePool.Add(characteristicEntity);
                ref var baseValueComponent = ref _baseValuePool.Add(characteristicEntity);
                ref var characteristicOwnerComponent = ref _characteristicsOwnerPool.Add(characteristicEntity);
                ref var ownerComponent = ref _ownerPool.Add(characteristicEntity);
                ref var defaultComponent = ref _defaultValuePool.Add(characteristicEntity);
                ref var previousValue = ref _previousValuePool.Add(characteristicEntity);
                ref var changedComponent = ref _changedPool.Add(characteristicEntity);
                ref var percentModificationsValueComponent = ref _modificationPercentPool.Add(characteristicEntity);
                ref var maxLimitValueComponent = ref _maxLimitModificationsPool.Add(characteristicEntity);
                ref var valueModificationsValueComponent = ref _valueModificationsPool.Add(characteristicEntity);

                var maxValue = requestComponent.MaxValue;
                var minValue = requestComponent.MinValue;
                var value = requestComponent.Value;

                maxValue = maxValue <= minValue ? minValue : maxValue;
                minValue = minValue >= maxValue ? maxValue : minValue;

                characteristicComponent.Value =value;
                minComponent.Value = minValue;
                maxComponent.Value = maxValue;
                baseValueComponent.Value = value;
                characteristicOwnerComponent.Link = requestComponent.Owner;
                ownerComponent.Value = requestComponent.Owner;
                
                defaultComponent.Value =     value;
                defaultComponent.BaseValue = value;
                defaultComponent.MinValue =  minValue;
                defaultComponent.MaxValue =  maxValue;

                ref var characteristicLinkComponent = ref _characteristicLinkPool.Add(ownerEntity);
                ref var valueComponent = ref _characteristicValuePool.Add(ownerEntity);
                
                characteristicLinkComponent.Value = characteristicsPacked;
                
                valueComponent.Value =     value;
                valueComponent.BaseValue = value;
                valueComponent.MaxValue =  maxValue;
                valueComponent.MinValue =  minValue;
            }
        }
    }
}