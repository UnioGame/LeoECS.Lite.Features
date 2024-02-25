namespace Game.Ecs.Ability.Aspects
{
    using System;
    using Common.Components;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
    public class AbilityOwnerAspect : EcsAspect
    {
        public EcsPool<AbilityInHandLinkComponent> AbilityInHandLink;
        public EcsPool<AbilityMapComponent> AbilityMap;
        public EcsPool<AbilityInProcessingComponent> AbilityInProcessing;
        public EcsPool<AbilitySlotComponent> Slot;
        
        //requests
        
        public EcsPool<ApplyAbilitySelfRequest> ApplyAbility;
        public EcsPool<SetInHandAbilitySelfRequest> SetInHandAbility;
        public EcsPool<ApplyAbilityBySlotSelfRequest> ApplyAbilityBySlot;
    }
}