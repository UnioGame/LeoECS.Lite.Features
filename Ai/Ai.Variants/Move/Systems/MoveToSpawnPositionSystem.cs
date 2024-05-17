namespace Game.Ecs.GameAi.Move.Systems
{
    using System;
    using AI.TargetOverride.Components;
    using Components;
    using global::Ai.Ai.Variants.MoveToTarget.Aspects;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class MoveToSpawnPositionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;

        private MoveAspect _moveAspect;

        private EcsPool<TEMPORARY_SpawnPositionComponent> _spawnPositionComponent;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<MoveToSpawnPositionComponent>()
                .Inc<TEMPORARY_SpawnPositionComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var moveToSpawnPositionComponent = ref _moveAspect.SpawnPosition.Get(entity);
                ref var spawnPositionComponent = ref _spawnPositionComponent.Get(entity);
                ref var moveToTargetComponent = ref _moveAspect.Target.GetOrAddComponent(entity);

                if (moveToSpawnPositionComponent.Priority > moveToTargetComponent.Priority)
                {
                    moveToTargetComponent.Priority = moveToSpawnPositionComponent.Priority;
                    moveToTargetComponent.Value = spawnPositionComponent.Value;
                }
            }
        }
    }
}