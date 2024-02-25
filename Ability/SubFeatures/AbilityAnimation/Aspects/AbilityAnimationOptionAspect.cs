namespace Game.Ecs.Ability.SubFeatures.AbilityAnimation.Aspects
{
    using System;
    using Components;
    using Core.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
    public class AbilityAnimationOptionAspect : EcsAspect
    {
        public EcsPool<AbilityAnimationOptionComponent> Option;
        public EcsPool<AnimationDataLinkComponent> Data;
        public EcsPool<OwnerComponent> Owner;
    }
}