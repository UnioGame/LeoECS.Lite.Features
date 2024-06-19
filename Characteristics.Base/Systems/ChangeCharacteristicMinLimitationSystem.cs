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
    public class ChangeCharacteristicMinLimitationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _changeRequestFilter;
        
        private EcsPool<ChangeMinLimitRequest> _requestPool;
        
        private EcsPool<MinValueComponent> _minPool;
        private EcsPool<MaxValueComponent> _maxPool;
        private EcsPool<RecalculateCharacteristicSelfRequest> _recalculatePool;
        private EcsPool<CharacteristicBaseValueComponent> _basePool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _changeRequestFilter = _world
                .Filter<ChangeMinLimitRequest>()
                .End();

            _requestPool = _world.GetPool<ChangeMinLimitRequest>();
            _basePool = _world.GetPool<CharacteristicBaseValueComponent>();
            _minPool = _world.GetPool<MinValueComponent>();
            _maxPool = _world.GetPool<MaxValueComponent>();
            _recalculatePool = _world.GetPool<RecalculateCharacteristicSelfRequest>();
        }

        public void Run(IEcsSystems systems)
        {

            foreach (var requestEntity in _changeRequestFilter)
            {
                ref var requestComponent = ref _requestPool.Get(requestEntity);
                var value = requestComponent.Value;
                
                if(!requestComponent.Target.Unpack(_world,out var characteristicEntity))
                    continue;
                
                if(!_basePool.Has(characteristicEntity))
                    continue;
                
                ref var minComponent = ref _minPool.Get(characteristicEntity);
                ref var maxComponent = ref _maxPool.Get(characteristicEntity);
                ref var baseValueComponent = ref _basePool.Get(characteristicEntity);

                minComponent.Value = value >= maxComponent.Value ? minComponent.Value : value;
                baseValueComponent.Value = Mathf.Clamp(baseValueComponent.Value, minComponent.Value, maxComponent.Value);
                
                _recalculatePool.GetOrAddComponent(characteristicEntity);
            }
            
        }
    }
}