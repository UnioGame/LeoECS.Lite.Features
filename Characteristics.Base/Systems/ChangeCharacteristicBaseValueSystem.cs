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
    public class ChangeCharacteristicBaseValueSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        
        private EcsPool<ChangeCharacteristicBaseRequest> _requestPool;
        private EcsPool<CharacteristicValueComponent> _characteristicPool;
        private EcsPool<MinValueComponent> _minPool;
        private EcsPool<MaxValueComponent> _maxPool;
        private EcsPool<CharacteristicBaseValueComponent> _baseValuePool;
        private EcsPool<RecalculateCharacteristicSelfRequest> _recalculatePool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world
                .Filter<ChangeCharacteristicBaseRequest>()
                .End();

            _requestPool = _world.GetPool<ChangeCharacteristicBaseRequest>();
            _characteristicPool = _world.GetPool<CharacteristicValueComponent>();
            _minPool = _world.GetPool<MinValueComponent>();
            _baseValuePool = _world.GetPool<CharacteristicBaseValueComponent>();
            _maxPool = _world.GetPool<MaxValueComponent>();
            _recalculatePool = _world.GetPool<RecalculateCharacteristicSelfRequest>();
        }

        public void Run(IEcsSystems systems)
        {

            foreach (var requestEntity in _filter)
            {
                ref var requestComponent = ref _requestPool.Get(requestEntity);
                var value = requestComponent.Value;
                
                if(!requestComponent.Target.Unpack(_world,out var characteristicEntity))
                    continue;
                
                if(!_characteristicPool.Has(characteristicEntity))
                    continue;
                
                ref var minComponent = ref _minPool.Get(characteristicEntity);
                ref var maxComponent = ref _maxPool.Get(characteristicEntity);
                ref var baseValueComponent = ref _baseValuePool.Get(characteristicEntity);
                
                var currentValue = baseValueComponent.Value;
                currentValue += value;
                currentValue = Mathf.Clamp(currentValue, minComponent.Value,maxComponent.Value);
                baseValueComponent.Value = currentValue;
                
                _recalculatePool.GetOrAddComponent(characteristicEntity);
            }
            
        }
    }
}