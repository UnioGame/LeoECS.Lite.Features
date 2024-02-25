namespace Game.Ecs.Gameplay.Death.Systems
{
    using System;
    using Animations.Aspects;
    using Aspects;
    using Characteristics.Health.Components;
    using Core.Components;
    using Core.Death.Components;
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
    public sealed class CompleteDeathAnimationSystem : IEcsRunSystem,IEcsInitSystem
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
                .Inc<DeathCompletedComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var playableDirector = ref _deathAspect.Director.Get(entity);

                if (_deathAspect.Champion.Has(entity)) continue;
                
                playableDirector.Value.Stop();
                playableDirector.Value.playableAsset = null;
                    
                _deathAspect.Evaluate.Del(entity);
                _deathAspect.AwaitDeath.TryRemove(entity);
            }
        }
    }
}