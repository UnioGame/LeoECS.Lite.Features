namespace Game.Ecs.Animations.Aspects
{
    using System;
    using Characteristics.Duration.Components;
    using Components;
    using Components.Requests;
    using Core.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
    public class PlayableAnimatorAspect : EcsAspect
    {
        public EcsPool<PlayableDirectorComponent> Director;
        public EcsPool<AnimatorPlayingComponent> PlayingTarget;
        public EcsPool<DurationComponent> Duration;
        
        //optional / statuses
        public EcsPool<AnimationPlayingComponent> Playing;


        public EcsPool<PlayOnDirectorSelfRequest> Play;
        public EcsPool<StopAnimationSelfRequest> StopSelf;
    }
}