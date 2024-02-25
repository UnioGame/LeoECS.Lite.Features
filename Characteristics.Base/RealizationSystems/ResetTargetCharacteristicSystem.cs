namespace Game.Ecs.Characteristics.Base.RealizationSystems
{
    using System;
    using Components;
    using Components.Requests;
    using Components.Requests.OwnerRequests;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;

    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif
    
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    /// reset target characteristic value to default
    [Serializable]
    public class ResetTargetCharacteristicSystem<TCharacteristic> : IEcsInitSystem, IEcsRunSystem
        where TCharacteristic : struct
    {
        private EcsWorld _world;
        private EcsFilter _requestFiler;
        private EcsPool<ResetCharacteristicRequest> _resetPool;
        private EcsPool<CharacteristicLinkComponent<TCharacteristic>> _linkPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _requestFiler = _world
                .Filter<ResetCharacteristicSelfRequest<TCharacteristic>>()
                .End();

            _linkPool = _world.GetPool<CharacteristicLinkComponent<TCharacteristic>>();
            _resetPool = _world.GetPool<ResetCharacteristicRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var requestEntity in _requestFiler)
            {
                if(!_linkPool.Has(requestEntity))
                    continue;
                
                ref var linkComponent = ref _linkPool.Get(requestEntity);
                
                var entity = _world.NewEntity();
                ref var resetComponent = ref _resetPool.Add(entity);
                resetComponent.Target = linkComponent.Value;
            }
        }
    }
    
    
    /// reset target characteristic value to default
    /// </summary>
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class ResetTargetCharacteristicMaxLimitSystem<TCharacteristic> : IEcsInitSystem, IEcsRunSystem
        where TCharacteristic : struct
    {
        private EcsWorld _world;
        private EcsFilter _requestFiler;
        private EcsPool<ResetCharacteristicMaxLimitSelfRequest> _resetPool;
        private EcsPool<CharacteristicLinkComponent<TCharacteristic>> _linkPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _requestFiler = _world
                .Filter<ResetCharacteristicMaxLimitSelfRequest<TCharacteristic>>()
                .Inc<CharacteristicComponent<TCharacteristic>>()
                .End();

            _linkPool = _world.GetPool<CharacteristicLinkComponent<TCharacteristic>>();
            _resetPool = _world.GetPool<ResetCharacteristicMaxLimitSelfRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var requestEntity in _requestFiler)
            {
                if(!_linkPool.Has(requestEntity))
                    continue;
                
                ref var linkComponent = ref _linkPool.Get(requestEntity);
                if(!linkComponent.Value.Unpack(_world,out var characteristicEntity))
                    continue;
                
                _resetPool.GetOrAddComponent(characteristicEntity);
            }
        }
    }
}