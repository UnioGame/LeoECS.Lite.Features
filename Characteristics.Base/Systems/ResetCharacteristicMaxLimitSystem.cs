namespace Game.Ecs.Characteristics.Base.Systems
{
    using System;
    using Components;
    using Components.Requests;
    using Game.Modules.UnioModules.UniGame.LeoEcsLite.LeoEcs.Shared.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// reset max value of characteristic
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class ResetCharacteristicMaxLimitSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        
        private EcsPool<MaxValueComponent> _maxValuePool;
        private EcsPool<CharacteristicDefaultValueComponent> _defaultValuePool;
        private EcsPool<RecalculateCharacteristicSelfRequest> _recalculateCharacteristicPool;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world
                .Filter<ResetCharacteristicMaxLimitSelfRequest>()
                .Inc<CharacteristicDefaultValueComponent>()
                .Inc<MaxValueComponent>()
                .End();

            _maxValuePool = _world.GetPool<MaxValueComponent>();
            _defaultValuePool = _world.GetPool<CharacteristicDefaultValueComponent>();
            _recalculateCharacteristicPool = _world.GetPool<RecalculateCharacteristicSelfRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var maxComponent = ref _maxValuePool.Get(entity);
                ref var defaultComponent = ref _defaultValuePool.Get(entity);

                maxComponent.Value = defaultComponent.MaxValue;

                _recalculateCharacteristicPool.GetOrAddComponent(entity);
            }
        }
    }
}