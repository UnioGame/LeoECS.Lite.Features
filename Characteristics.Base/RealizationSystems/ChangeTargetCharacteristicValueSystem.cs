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
    public class ChangeTargetCharacteristicValueSystem<TCharacteristic> : IEcsInitSystem, IEcsRunSystem
        where TCharacteristic : struct
    {
        private EcsWorld _world;
        private EcsFilter _changeRequestFilter;
        
        private EcsPool<ChangeCharacteristicValueRequest<TCharacteristic>> _requestPool;
        private EcsPool<ChangeCharacteristicRequest> _changePool;
        private EcsPool<CharacteristicLinkComponent<TCharacteristic>> _linkPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _changeRequestFilter = _world
                .Filter<ChangeCharacteristicValueRequest<TCharacteristic>>()
                .End();

            _requestPool = _world.GetPool<ChangeCharacteristicValueRequest<TCharacteristic>>();
            _changePool = _world.GetPool<ChangeCharacteristicRequest>();
            _linkPool = _world.GetPool<CharacteristicLinkComponent<TCharacteristic>>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var requestEntity in _changeRequestFilter)
            {
                ref var requestComponent = ref _requestPool.Get(requestEntity);
                if(!requestComponent.Target.Unpack(_world,out var characteristicEntity))
                    continue;
                
                if(!_linkPool.Has(characteristicEntity)) continue;

                ref var linkComponent = ref _linkPool.Get(characteristicEntity);

                var targetEntity = _world.NewEntity();
                ref var targetRequest = ref _changePool.Add(targetEntity);
                targetRequest.Target = linkComponent.Value;
                targetRequest.Source = requestComponent.Source;
                targetRequest.Value = requestComponent.Value;
            }
            
        }
    }
}