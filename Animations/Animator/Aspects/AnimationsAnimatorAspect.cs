namespace Animations.Animatror.Aspects
{
    using System;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    using UniGame.LeoEcs.Shared.Components;

    /// <summary>
    /// Animaitons Animator Aspect
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class AnimationsAnimatorAspect : EcsAspect
    {
        public EcsPool<PlayAnimationByIdSelfRequest> PlaySelf;
        public EcsPool<AnimationIsLiveComponent> CurrentlyPlaying;
        public EcsPool<AnimatorComponent> Animator;
        
    }
}