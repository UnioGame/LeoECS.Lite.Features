namespace Game.Ecs.Characteristics.Base.Systems
{
    using System;
    using Components;
    using Components.Requests;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// check modifications is tracked modification source is dead, then remove modification
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class CheckModificationSourceLifeTimeComponent : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<ModificationSourceLinkComponent> _sourceLinkPool;
        private EcsPool<CharacteristicLinkComponent> _characteristicsLinkPool;
        private EcsPool<RecalculateCharacteristicSelfRequest> _recalculateSelfPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world.Filter<ModificationComponent>()
                .Inc<ModificationSourceTrackComponent>()
                .Inc<CharacteristicLinkComponent>()
                .Inc<ModificationSourceLinkComponent>()
                .End();

            _sourceLinkPool = _world.GetPool<ModificationSourceLinkComponent>();
            _characteristicsLinkPool = _world.GetPool<CharacteristicLinkComponent>();
            _recalculateSelfPool = _world.GetPool<RecalculateCharacteristicSelfRequest>();
            
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var modification in _filter)
            {
                ref var ownerLinkComponent = ref _sourceLinkPool.Get(modification);
                if (ownerLinkComponent.Value.Unpack(_world,out var sourceEntity))
                    continue;

                ref var characteristicsLinkComponent = ref _characteristicsLinkPool.Get(modification);

                if (characteristicsLinkComponent.Link.Unpack(_world, out var characteristicEntity))
                {
                    ref var recalculateRequest = ref _recalculateSelfPool.GetOrAddComponent(characteristicEntity);
                }
                
                _world.DelEntity(modification);
            }
        }
    }
}