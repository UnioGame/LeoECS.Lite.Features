namespace Game.Ecs.Ability.SubFeatures.AbilitySequence.Bahaviours.Aspects
{
    using System;
    using Components;
    using Core.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
    public class AbilitySequenceOnStartAspect : EcsAspect
    {
        public EcsPool<ActivateSequenceTriggerComponent> ActivateTrigger;
        public EcsPool<OwnerComponent> Owner;
    }
}