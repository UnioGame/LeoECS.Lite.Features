namespace Game.Ecs.Animations.Aspects
{
    using System;
    using Characteristics.Duration.Components;
    using Components;
    using Components.Requests;
    using Core.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Timer.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
    public class AnimationTimelineAspect : EcsAspect
    {
        //animation duration
        public EcsPool<DurationComponent> Duration;
        public EcsPool<OwnerComponent> Owner;
        //playable director source entity
        public EcsPool<AnimationTargetComponent> Target;
        //playable data
        public EcsPool<AnimationPlayableComponent> Animation;
        
        public EcsPool<AnimationWrapModeComponent> WrapMode;
        public EcsPool<AnimationPaybackSpeedComponent> Speed;
        public EcsPool<AnimationStartTimeComponent> StartTime;
        
        //optional
        public EcsPool<AnimationBindingDataComponent> Binding;
        //animation execution cooldown
        public EcsPool<CooldownComponent> Cooldown;
        public EcsPool<CooldownStateComponent> CooldownState;
        public EcsPool<AnimationLinkComponent> Link;
        
        //animation is active and can be played
        public EcsPool<AnimationReadyComponent> Ready;
        //animation current playing
        public EcsPool<AnimationPlayingComponent> Playing;
        //kill animation entity on complete
        public EcsPool<AnimationKillOnComplete> KillOnComplete;
        
        public EcsPool<AnimationCompleteComponent> Complete;
        
        public EcsPool<CreateAnimationLinkSelfRequest> CreateLinkSelfAnimation;
        public EcsPool<CreateAnimationPlayableSelfRequest> CreateSelfAnimation;
        
        //start exists animation entity
        public EcsPool<PlayAnimationSelfRequest> PlaySelf;
        //stop animation
        public EcsPool<StopAnimationSelfRequest> StopSelf;
    }
}