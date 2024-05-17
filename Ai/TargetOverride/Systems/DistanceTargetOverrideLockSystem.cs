namespace Game.Ecs.AI.TargetOverride.Systems
{
    using System;
    using Aspects;
    using Components;
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
    public class DistanceTargetOverrideLockSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;

        private TargetOverrideAspect _targetOverrideAspect;
        private TargetSelectionAspect _targetSelectionAspect;

        private EcsPool<TransformPositionComponent> _transformPositionPool;
        private EcsPool<TEMPORARY_SpawnPositionComponent> _spawnPositionPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<DistanceTargetOverrideLockComponent>()
                .Inc<TEMPORARY_SpawnPositionComponent>()
                .Inc<TransformPositionComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var distanceTargetOverrideLockComponent = ref _targetOverrideAspect.DistanceOverrideLock.Get(entity);
                ref var transformPositionComponent = ref _transformPositionPool.Get(entity);
                ref var spawnPositionComponent = ref _spawnPositionPool.Get(entity);
                var distanceSqr = math.distancesq(transformPositionComponent.Position, spawnPositionComponent.Value);
                if (distanceSqr >= distanceTargetOverrideLockComponent.LockDistanceSqr)
                {
                    _targetOverrideAspect.OverrideLock.GetOrAddComponent(entity);
                    _targetSelectionAspect.LayerOverride.TryRemove(entity);
                }
                else if (distanceSqr <= distanceTargetOverrideLockComponent.ReleaseDistanceSqr)
                {
                    _targetOverrideAspect.OverrideLock.TryRemove(entity);
                }
            }
        }
    }
}