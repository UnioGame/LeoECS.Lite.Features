namespace Game.Ecs.Gameplay.Death.Systems
{
    using System;
    using Animations.Aspects;
    using Aspects;
    using Core.Components;
    using Game.Ecs.Core.Death.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class EvaluateDeathAnimationSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        private DeathAspect _deathAspect;
        private AnimationTimelineAspect _animationAspect;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<DeadAnimationEvaluateComponent>()
                .Inc<DeathAnimationComponent>()
                .Inc<PlayableDirectorComponent>()
                .Exc<DeathCompletedComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var evaluate = ref _deathAspect.Evaluate.Get(entity);

                if (!evaluate.Value.Unpack(_world, out var animationEntity))
                {
                    _deathAspect.Completed.Add(entity);
                    continue;
                }

                if (!_animationAspect.Complete.Has(animationEntity)) continue;
                
                _deathAspect.Completed.Add(entity);
            }
        }
    }
}