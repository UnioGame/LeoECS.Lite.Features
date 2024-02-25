namespace Game.Ecs.Characteristics.Base.RealizationSystems
{
    using System;
    using Components;
    using Components.Requests;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class RecalculateCharacteristicSystem<TCharacteristic> : IEcsInitSystem, IEcsRunSystem
        where TCharacteristic : struct
    {
        private EcsWorld _world;
        private EcsFilter _requestFilter;
        private EcsPool<RecalculateCharacteristicSelfRequest<TCharacteristic>> _requestPool;
        private EcsPool<RecalculateCharacteristicSelfRequest> _recalculatePool;
        private EcsPool<CharacteristicLinkComponent<TCharacteristic>> _linkPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _requestFilter = _world
                .Filter<RecalculateCharacteristicSelfRequest<TCharacteristic>>()
                .Inc<CharacteristicLinkComponent<TCharacteristic>>()
                .End();

            _linkPool = _world.GetPool<CharacteristicLinkComponent<TCharacteristic>>();
            _recalculatePool = _world.GetPool<RecalculateCharacteristicSelfRequest>();
            _requestPool = _world.GetPool<RecalculateCharacteristicSelfRequest<TCharacteristic>>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var requestEntity in _requestFilter)
            {
                ref var linkComponent = ref _linkPool.Get(requestEntity);
                
                if(!linkComponent.Value.Unpack(_world,out var linkEntity))
                    continue;

                _recalculatePool.GetOrAddComponent(linkEntity);
                _requestPool.Del(requestEntity);
            }
        }
    }
}