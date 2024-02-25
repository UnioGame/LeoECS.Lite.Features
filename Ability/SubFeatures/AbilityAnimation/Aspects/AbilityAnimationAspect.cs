namespace Game.Ecs.Ability.SubFeatures.AbilityAnimation.Aspects
{
    using System;
    using Animations.Components;
    using Characteristics.Cooldown.Components;
    using Characteristics.Duration.Components;
    using Components;
    using Core.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Timer.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
    public class AbilityAnimationAspect : EcsAspect
    {
        public EcsPool<OwnerComponent> Owner;
        public EcsPool<AnimationDataLinkComponent> Data;
        public EcsPool<AbilityActiveAnimationComponent> ActiveAnimation;
        public EcsPool<LinkToAnimationComponent> AnimationLink;
        public EcsPool<DurationComponent> Duration;
        public EcsPool<CooldownComponent> Cooldown;
        public EcsPool<PlayableDirectorComponent> Director;
    }
}