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
    public class MoveToDefaultTargetSystem : IEcsInitSystem, IEcsRunSystem
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
                .Filter<MoveToDefaultTargetComponent>()
                .Inc<DefaultTargetComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var defaultEntity in _filter)
            {
                ref var defaultTargetComponent = ref _prioritizerAspect.Default.Get(defaultEntity);
                if (!defaultTargetComponent.Value.Unpack(_world, out var defaultTargetEntity))
                {
                    continue;
                }
                
                ref var moveToDefaultTargetComponent = ref _moveAspect.DefaultTarget.Get(defaultEntity);
                ref var moveToTargetComponent = ref _moveAspect.Target.GetOrAddComponent(defaultEntity);
                if (moveToDefaultTargetComponent.Priority > moveToTargetComponent.Priority)
                {
                    ref var targetPositionComponent = ref _transformPositionPool.Get(defaultTargetEntity);
                    
                    moveToTargetComponent.Priority = moveToDefaultTargetComponent.Priority;
                    moveToTargetComponent.Value = targetPositionComponent.Position;
                }
            }
        }
    }
}