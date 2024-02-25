namespace Game.Ecs.Characteristics.Base.Systems
{
    using System;
    using Components;
    using Components.Requests;
    using Game.Modules.UnioModules.UniGame.LeoEcsLite.LeoEcs.Shared.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    /// <summary>
    /// changed base value of characteristics
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class ChangeCharacteristicValueSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _changeRequestFilter;
        
        private EcsPool<ChangeCharacteristicRequest> _requestPool;
        private EcsPool<CharacteristicValueComponent> _characteristicPool;
        private EcsPool<MinValueComponent> _minPool;
        private EcsPool<MaxValueComponent> _maxPool;
        
        private EcsPool<CharacteristicChangedComponent> _changedPool;
        private EcsPool<CharacteristicPreviousValueComponent> _previousPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _changeRequestFilter = _world
                .Filter<ChangeCharacteristicRequest>()
                .End();

            _requestPool = _world.GetPool<ChangeCharacteristicRequest>();
            _characteristicPool = _world.GetPool<CharacteristicValueComponent>();
            _minPool = _world.GetPool<MinValueComponent>();
            _maxPool = _world.GetPool<MaxValueComponent>();
            _previousPool = _world.GetPool<CharacteristicPreviousValueComponent>();
            
            _changedPool = _world.GetPool<CharacteristicChangedComponent>();
        }

        public void Run(IEcsSystems systems)
        {

            foreach (var requestEntity in _changeRequestFilter)
            {
                ref var requestComponent = ref _requestPool.Get(requestEntity);
                var value = requestComponent.Value;
                
                if(!requestComponent.Target.Unpack(_world,out var characteristicEntity))
                    continue;
                
                if(!_characteristicPool.Has(characteristicEntity))
                    continue;
                
                ref var minComponent = ref _minPool.Get(characteristicEntity);
                ref var maxComponent = ref _maxPool.Get(characteristicEntity);
                ref var valueComponent = ref _characteristicPool.Get(characteristicEntity);
  
                var previousValue = valueComponent.Value;
                var currentValue = valueComponent.Value;
                currentValue += value;
                currentValue = Mathf.Clamp(currentValue, minComponent.Value, maxComponent.Value);
                valueComponent.Value = currentValue;

                //mark as changed
                ref var  eventComponent = ref _changedPool.GetOrAddComponent(characteristicEntity);
                eventComponent.Value = currentValue;
                eventComponent.PreviousValue = previousValue;
            }
            
        }
    }
}