namespace Game.Ecs.Ai.Targeting.Systems
{
    using System;
    using AI.TargetOverride.Components;
    using Aspects;
    using Components;
    using global::Ai.Ai.Variants.Prioritizer.Aspects;
    using Leopotam.EcsLite;
    using TargetSelection.Aspects;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using Unity.Mathematics;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class OverDistanceTargetingLockSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;

        private TargetingAspect _targetingAspect;
        private TargetSelectionAspect _targetSelectionAspect;
        private PrioritizerAspect _prioritizerAspect;
        
        private EcsPool<TransformPositionComponent> _transformPositionPool;
        private EcsPool<TEMPORARY_SpawnPositionComponent> _spawnPositionPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<OverDistanceTargetingLockComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var overDistanceLockComponent = ref _targetingAspect.OverDistanceLock.Get(entity);
                ref var transformPositionComponent = ref _transformPositionPool.Get(entity);
                ref var spawnPositionComponent = ref _spawnPositionPool.Get(entity);
                var distanceSqr = math.distancesq(transformPositionComponent.Position, spawnPositionComponent.Value);
                if (distanceSqr >= overDistanceLockComponent.LockDistanceSqr)
                {
                    _targetingAspect.Lock.TryAdd(entity);
                    _prioritizerAspect.Agro.TryRemove(entity);
                    if (_targetSelectionAspect.TargetSelectionResult.Has(entity))
                    {
                        ref var resultComponent = ref _targetSelectionAspect.TargetSelectionResult.Get(entity);
                        resultComponent.Count = 0;
                    }
                }
                else if (distanceSqr <= overDistanceLockComponent.ReleaseDistanceSqr)
                {
                    _targetingAspect.Lock.TryRemove(entity);
                }
            }
        }
    }
}