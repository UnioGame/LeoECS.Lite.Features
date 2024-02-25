namespace Game.Ecs.Characteristics.Base.RealizationSystems
{
    using System;
    using Components;
    using Components.Requests;
    using Components.Requests.OwnerRequests;
    using Leopotam.EcsLite;

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
    public class ChangeTargetCharacteristicMaxLimitationSystem<TCharacteristic> : IEcsInitSystem, IEcsRunSystem
        where TCharacteristic : struct
    {
        private EcsWorld _world;
        private EcsFilter _changeRequestFilter;
        
        private EcsPool<ChangeMaxLimitSelfRequest<TCharacteristic>> _requestPool;
        private EcsPool<CharacteristicLinkComponent<TCharacteristic>> _linkPool;
        private EcsPool<ChangeMaxLimitRequest> _limitPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _changeRequestFilter = _world
                .Filter<ChangeMaxLimitSelfRequest<TCharacteristic>>()
                .Inc<CharacteristicLinkComponent<TCharacteristic>>()
                .End();

            _requestPool = _world.GetPool<ChangeMaxLimitSelfRequest<TCharacteristic>>();
            _linkPool = _world.GetPool<CharacteristicLinkComponent<TCharacteristic>>();
            _limitPool = _world.GetPool<ChangeMaxLimitRequest>();
        }

        public void Run(IEcsSystems systems)
        {

            foreach (var requestEntity in _changeRequestFilter)
            {
                ref var requestComponent = ref _requestPool.Get(requestEntity);
                ref var linkComponent = ref _linkPool.Get(requestEntity);
             
                if(!linkComponent.Value.Unpack(_world,out var characteristicEntity))
                    continue;

                var targetRequestEntity = _world.NewEntity();
                ref var targetRequestComponent = ref _limitPool.Add(targetRequestEntity);
                targetRequestComponent.Target = linkComponent.Value;
                targetRequestComponent.Value = requestComponent.Value;
            }
            
        }
    }
}