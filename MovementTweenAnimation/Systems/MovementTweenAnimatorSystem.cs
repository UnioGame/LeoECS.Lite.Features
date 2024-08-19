namespace MovementTweenAnimation.Systems
{
    using System;
    using Animations.Animatror.Aspects;
    using Aspects;
    using Components;
    using Game.Ecs.Characteristics.Speed.Components;
    using Game.Ecs.Core.Components;
    using Game.Ecs.Movement.Aspect;
    using Game.Ecs.Movement.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// Performs movement tween animations for entities.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class MovementTweenAnimatorSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private MovementTweenAnimationAspect _aspect;
        private MovementAspect _movementAspect;
        
        private AnimationsAnimatorAspect _animatorAspect;
        private EcsFilter _deathFilter;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world
                .Filter<MovementTweenAnimationComponent>()
                .Inc<MovementAgentComponent>()
                .Inc<SpeedComponent>()
                .Exc<ImmobilityComponent>()
                .Exc<PrepareToDeathComponent>()
                .End();

            _deathFilter = _world
                .Filter<PrepareToDeathComponent>()
                .Inc<MovementTweenAnimationComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var speedComponent = ref _aspect.Speed.Get(entity);
                var speed = speedComponent.Value;

                ref var movementTweenAnimationComponent = ref _aspect.MoveAnimation.Get(entity);
                var scaleTween = movementTweenAnimationComponent.MoveTween;

                if (speed != 0)
                    scaleTween.Play();
                else
                    scaleTween.Reset();
            }

            foreach (var entity in _deathFilter)
            {
                ref var movementTweenAnimationComponent = ref _aspect.MoveAnimation.Get(entity);
                var scaleTween = movementTweenAnimationComponent.MoveTween;
                
                scaleTween.Stop();
            }
        }
    }
}