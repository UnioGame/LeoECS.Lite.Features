namespace Game.Ecs.GameAi.Move.Systems
{
    using System;
    using Components;
    using global::Ai.Ai.Variants.MoveToTarget.Aspects;
    using global::Ai.Ai.Variants.Prioritizer.Aspects;
    using global::Ai.Ai.Variants.Prioritizer.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class MoveToChaseTargetSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter _filter;
        private MoveAspect _moveAspect;
        private PrioritizerAspect _prioritizerAspect;

        private EcsPool<TransformPositionComponent> _transformPositionPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<MoveToChaseTargetComponent>()
                .Inc<ChaseTargetComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var chaseEntity in _filter)
            {
                ref var chaseTargetComponent = ref _prioritizerAspect.Chase.Get(chaseEntity);
                if (!chaseTargetComponent.Value.Unpack(_world, out var chaseTargetEntity))
                {
                    continue;
                }
                
                ref var moveToChaseTargetComponent = ref _moveAspect.ChaseTarget.Get(chaseEntity);
                ref var moveToTargetComponent = ref _moveAspect.Target.GetOrAddComponent(chaseEntity);
                if (moveToChaseTargetComponent.Priority > moveToTargetComponent.Priority)
                {
                    ref var targetPositionComponent = ref _transformPositionPool.Get(chaseTargetEntity);
                    moveToTargetComponent.Priority = moveToChaseTargetComponent.Priority;
                    moveToTargetComponent.Value = targetPositionComponent.Position;
                }
            }
        }
    }
}