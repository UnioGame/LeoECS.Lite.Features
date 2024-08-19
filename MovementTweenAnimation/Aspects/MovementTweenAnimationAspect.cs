namespace MovementTweenAnimation.Aspects
{
    using System;
    using Components;
    using Game.Ecs.Characteristics.Speed.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    using UniGame.LeoEcs.Shared.Components;

    /// <summary>
    /// Aspect for movement tween animation.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class MovementTweenAnimationAspect : EcsAspect
    {
        // UniModules
        public EcsPool<SpeedComponent> Speed;
        public EcsPool<AnimatorComponent> Animator;
        
        // Game
        public EcsPool<MovementTweenAnimationComponent> MoveAnimation;
    }
}
