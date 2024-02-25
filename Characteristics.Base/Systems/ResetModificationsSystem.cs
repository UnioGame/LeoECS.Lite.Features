namespace Game.Ecs.Characteristics.Base.Systems
{
    using System;
    using Components;
    using Components.Requests;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// reset all modifications from characteristic
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class ResetModificationsSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _requestFilter;
        private EcsFilter _modificationsFilter;
        
        private EcsPool<ResetModificationsRequest> _resetPool;
        private EcsPool<CharacteristicValueComponent> _characteristicPool;
        
        private EcsPool<CharacteristicLinkComponent> _linkPool;
        private EcsPool<RecalculateCharacteristicSelfRequest> _recalculatePool;
        private EcsPool<CharacteristicDefaultValueComponent> _defaultPool;
        private EcsPool<CharacteristicBaseValueComponent> _baseValuePool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _requestFilter = _world
                .Filter<ResetModificationsRequest>()
                .End();

            _modificationsFilter = _world
                .Filter<ModificationComponent>()
                .Inc<CharacteristicLinkComponent>()
                .End();

            _resetPool = _world.GetPool<ResetModificationsRequest>();
            _characteristicPool = _world.GetPool<CharacteristicValueComponent>();
            _linkPool = _world.GetPool<CharacteristicLinkComponent>();
            _recalculatePool = _world.GetPool<RecalculateCharacteristicSelfRequest>();
            _defaultPool = _world.GetPool<CharacteristicDefaultValueComponent>();
            _baseValuePool = _world.GetPool<CharacteristicBaseValueComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var requestEntity in _requestFilter)
            {
                ref var resetComponent = ref _resetPool.Get(requestEntity);
                ref var characteristic = ref resetComponent.Characteristic;
                _resetPool.Del(requestEntity);
                
                if(!characteristic.Unpack(_world,out var characteristicEntity))
                    continue;
                
                if(!_characteristicPool.Has(characteristicEntity)) continue;

                var isModificationChanged = false;
                
                //remove all modifications
                foreach (var modificationEntity in _modificationsFilter)
                {
                    ref var characteristicLinkComponent = ref _linkPool.Get(modificationEntity);
                    if(!characteristicLinkComponent.Link.Unpack(_world,out var characteristicLinkEntity))
                        continue;
                    if(characteristicLinkEntity != characteristicEntity) continue;
                    
                    isModificationChanged = true;
                    
                    _world.DelEntity(modificationEntity);
                }

                if (!isModificationChanged) return;

                _recalculatePool.GetOrAddComponent(characteristicEntity);
            }
        }
    }
}