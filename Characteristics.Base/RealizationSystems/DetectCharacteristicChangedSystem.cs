namespace Game.Ecs.Characteristics.Base.RealizationSystems
{
    using System;
    using Components;
    using Components.Events;
    using Game.Ecs.Core.Components;
    using Game.Modules.UnioModules.UniGame.LeoEcsLite.LeoEcs.Shared.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// update characteristic owner value by changed status
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class DetectCharacteristicChangedSystem<TCharacteristic> : IEcsInitSystem, IEcsRunSystem
        where TCharacteristic : struct
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        
        private EcsPool<CharacteristicValueChangedEvent<TCharacteristic>> _resultEventPool;
        private EcsPool<CharacteristicComponent<TCharacteristic>> _characteristicValuePool;
        
        private EcsPool<MinValueComponent> _minPool;
        private EcsPool<CharacteristicBaseValueComponent> _baseValuePool;
        private EcsPool<CharacteristicValueComponent> _valuePool;
        private EcsPool<MaxValueComponent> _maxPool;
        private EcsPool<OwnerComponent> _ownerPool;
        private EcsPool<CharacteristicChangedComponent<TCharacteristic>> _changedPool;
        private EcsPool<CharacteristicChangedComponent> _changedCharacteristicPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world
                .Filter<CharacteristicChangedComponent>()
                .Inc<CharacteristicOwnerComponent<TCharacteristic>>()
                .Inc<CharacteristicValueComponent>()
                .Inc<OwnerComponent>()
                .End();
            
            _resultEventPool = _world.GetPool<CharacteristicValueChangedEvent<TCharacteristic>>();
            _changedPool = _world.GetPool<CharacteristicChangedComponent<TCharacteristic>>();
            _characteristicValuePool = _world.GetPool<CharacteristicComponent<TCharacteristic>>();

            _minPool = _world.GetPool<MinValueComponent>();
            _baseValuePool = _world.GetPool<CharacteristicBaseValueComponent>();
            _maxPool = _world.GetPool<MaxValueComponent>();
            _ownerPool = _world.GetPool<OwnerComponent>();
            _changedCharacteristicPool = _world.GetPool<CharacteristicChangedComponent>();
            _valuePool = _world.GetPool<CharacteristicValueComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var characteristicEntity in _filter)
            {
                ref var ownerComponent = ref _ownerPool.Get(characteristicEntity);
                if(!ownerComponent.Value.Unpack(_world,out var ownerEntity))
                    continue;
                
                ref var characteristicChangedComponent = ref _changedPool.GetOrAddComponent(ownerEntity);
                ref var changedComponent = ref _changedCharacteristicPool.Get(characteristicEntity);

                characteristicChangedComponent.Value = changedComponent.Value;
                characteristicChangedComponent.PreviousValue = changedComponent.PreviousValue;
                
                ref var characteristicValueComponent = ref _valuePool.Get(characteristicEntity);
                ref var baseValueComponent = ref _baseValuePool.Get(characteristicEntity);
                ref var maxValueComponent = ref _maxPool.Get(characteristicEntity);
                ref var minValueComponent = ref _minPool.Get(characteristicEntity);
                
                ref var characteristicComponent = ref _characteristicValuePool.Get(ownerEntity);
                characteristicComponent.Value = characteristicValueComponent.Value;
                characteristicComponent.MinValue = minValueComponent.Value;
                characteristicComponent.MaxValue = maxValueComponent.Value;
                characteristicComponent.BaseValue = baseValueComponent.Value;
                    
                var newEventEntity = _world.NewEntity();
                ref var newEventComponent = ref _resultEventPool.Add(newEventEntity);
                
                newEventComponent.Owner = ownerComponent.Value;
                newEventComponent.Characteristic = _world.PackEntity(characteristicEntity);
                newEventComponent.Value = characteristicValueComponent.Value;
                newEventComponent.PreviousValue = changedComponent.PreviousValue;
            }
        }
    }
}