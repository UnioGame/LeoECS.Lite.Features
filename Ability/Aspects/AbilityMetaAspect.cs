namespace Game.Ecs.AbilityInventory.Aspects
{
    using System;
    using Ability.Common.Components;
    using Ability.Components;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
    public class AbilityMetaAspect : EcsAspect
    {
        public EcsPool<AbilityConfigurationReferenceComponent> ConfigurationReference;
        public EcsPool<AbilityConfigurationComponent> Configuration;
        public EcsPool<AbilityVisualComponent> Visual;
        public EcsPool<NameComponent> Name;
        public EcsPool<AbilityMetaComponent> Meta;
        public EcsPool<AbilitySlotComponent> Slot;
        public EcsPool<AbilityIdComponent> Id;
        public EcsPool<AbilityBlockedComponent> Blocked;
        //arena specific
        public EcsPool<AbilityCategoryComponent> Category;
        public EcsPool<AbilityLevelComponent> Level;
        public EcsPool<PassiveAbilityComponent> IsPassive;
    }
}