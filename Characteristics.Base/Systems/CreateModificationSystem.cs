namespace Game.Ecs.Characteristics.Base.Systems
{
    using System;
    using Components;
    using Components.Requests;
    using Game.Ecs.Core.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// create new modification for target characteristic
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class CreateModificationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        
        private EcsPool<CharacteristicLinkComponent> _characteristicLinkPool;
        private EcsPool<CreateModificationRequest> _requestPool;
        private EcsPool<ModificationComponent> _modificationPool;
        private EcsPool<ModificationSourceLinkComponent> _sourceLinkPool;
        private EcsPool<ModificationSourceTrackComponent> _trackComponentPool;
        private EcsPool<RecalculateCharacteristicSelfRequest> _recalculatePool;
        private EcsPool<ModificationPercentComponent> _percentPool;
        private EcsPool<OwnerComponent> _ownerPool;
        private EcsPool<ModificationMaxLimitComponent> _maxLimitPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world
                .Filter<CreateModificationRequest>()
                .End();
            
            _characteristicLinkPool = _world.GetPool<CharacteristicLinkComponent>();
            _requestPool = _world.GetPool<CreateModificationRequest>();
            _modificationPool = _world.GetPool<ModificationComponent>();
            _sourceLinkPool = _world.GetPool<ModificationSourceLinkComponent>();
            _trackComponentPool = _world.GetPool<ModificationSourceTrackComponent>();
            _percentPool = _world.GetPool<ModificationPercentComponent>();
            _maxLimitPool = _world.GetPool<ModificationMaxLimitComponent>();
            _recalculatePool = _world.GetPool<RecalculateCharacteristicSelfRequest>();
            _ownerPool = _world.GetPool<OwnerComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var requestEntity in _filter)
            {
                ref var requestComponent = ref _requestPool.Get(requestEntity);
                if (!requestComponent.Target.Unpack(_world, out var characteristicEntity))
                    continue;
                
                var modificationEntity = _world.NewEntity();
                
                ref var ownerComponent = ref _ownerPool.Add(modificationEntity);
                ref var modificationValueComponent = ref _modificationPool.Add(modificationEntity);
                ref var linkCharacteristicComponent = ref _characteristicLinkPool.Add(modificationEntity);
                ref var sourceLinkComponent = ref _sourceLinkPool.Add(modificationEntity);
                
                //mark modification as binded to source lifetime
                _trackComponentPool.Add(modificationEntity);
                
                var modificationValue = requestComponent.Modification;
                var counter = modificationValue.counter == 0 ? 1 : modificationValue.counter;

                ownerComponent.Value = requestComponent.Target;
                modificationValueComponent.IsPercent = modificationValue.isPercent;
                modificationValueComponent.AllowedSummation = modificationValue.allowedSummation;
                modificationValueComponent.BaseValue = modificationValue.baseValue;
                modificationValueComponent.Counter = counter;

                if (modificationValue.isPercent) _percentPool.Add(modificationEntity);
                if (modificationValue.isMaxLimitModification) _maxLimitPool.Add(modificationEntity);
                
                linkCharacteristicComponent.Link = requestComponent.Target;
                sourceLinkComponent.Value = requestComponent.ModificationSource;
                
                _recalculatePool.GetOrAddComponent(characteristicEntity);
            }
        }
    }
}