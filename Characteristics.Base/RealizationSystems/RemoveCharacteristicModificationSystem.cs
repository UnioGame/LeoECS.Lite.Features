namespace Game.Ecs.Characteristics.Base.RealizationSystems
{
    using System;
    using Components;
    using Components.Requests;
    using Components.Requests.OwnerRequests;
    using Leopotam.EcsLite;

    /// <summary>
    /// convert remove modification request to remove characteristic modification request
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class RemoveCharacteristicModificationSystem<TCharacteristic> : IEcsInitSystem, IEcsRunSystem
        where TCharacteristic : struct
    {
        private EcsWorld _world;
        private EcsFilter _requestFiler;
        private EcsPool<RemoveCharacteristicModificationRequest<TCharacteristic>> _requestPool;
        private EcsPool<RemoveModificationRequest> _modificationPool;
        private EcsPool<CharacteristicLinkComponent<TCharacteristic>> _linkPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _requestFiler = _world
                .Filter<RemoveCharacteristicModificationRequest<TCharacteristic>>()
                .End();

            _requestPool = _world.GetPool<RemoveCharacteristicModificationRequest<TCharacteristic>>();
            _linkPool = _world.GetPool<CharacteristicLinkComponent<TCharacteristic>>();
            _modificationPool = _world.GetPool<RemoveModificationRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var requestEntity in _requestFiler)
            {
                ref var requestComponent = ref _requestPool.Get(requestEntity);
                if(!requestComponent.Target.Unpack(_world,out var characteristicEntity))
                    continue;
                
                if(!_linkPool.Has(characteristicEntity))
                    continue;
                
                ref var linkComponent = ref _linkPool.Get(characteristicEntity);
                
                var entity = _world.NewEntity();
                ref var removeRequestComponent = ref _modificationPool.Add(entity);
                
                removeRequestComponent.Source = requestComponent.Source;
                removeRequestComponent.Characteristic = linkComponent.Value;
            }
        }
    }
}