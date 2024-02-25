namespace Game.Ecs.Characteristics.Base.Systems
{
    using System;
    using Components;
    using Components.Events;
    using Game.Ecs.Core.Components;
    using Game.Modules.UnioModules.UniGame.LeoEcsLite.LeoEcs.Shared.Components;
    using Leopotam.EcsLite;

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
    public class DetectCharacteristicChangesSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _changeRequestFilter;
        
        private EcsPool<CharacteristicChangedComponent> _changedPool;
        private EcsPool<OwnerComponent> _ownerPool;
        private EcsPool<CharacteristicPreviousValueComponent> _previousPool;
        
        private EcsPool<CharacteristicValueChangedEvent> _eventPool;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _changeRequestFilter = _world
                .Filter<CharacteristicChangedComponent>()
                .Inc<CharacteristicValueComponent>()
                .Inc<MinValueComponent>()
                .Inc<MaxValueComponent>()
                .Inc<CharacteristicBaseValueComponent>()
                .End();
            
            _changedPool = _world.GetPool<CharacteristicChangedComponent>();
            _eventPool = _world.GetPool<CharacteristicValueChangedEvent>();
            _ownerPool = _world.GetPool<OwnerComponent>();
            _previousPool = _world.GetPool<CharacteristicPreviousValueComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var changesEntity in _changeRequestFilter)
            {
                ref var changedComponent = ref _changedPool.Get(changesEntity);
                ref var previousValue = ref _previousPool.Get(changesEntity);
                ref var ownerComponent = ref _ownerPool.Get(changesEntity);
                
                var eventEntity = _world.NewEntity();
                ref var eventComponent = ref _eventPool.Add(eventEntity);
                eventComponent.Owner = ownerComponent.Value;
                eventComponent.Value = changedComponent.Value;
                eventComponent.PreviousValue = previousValue.Value;
                eventComponent.Characteristic = _world.PackEntity(changesEntity);
            }
        }
    }
}