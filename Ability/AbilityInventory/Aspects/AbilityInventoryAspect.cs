namespace Game.Ecs.AbilityInventory.Aspects
{
    using System;
    using Ability.Common.Components;
    using Ability.Components;
    using Ability.SubFeatures.AbilityAnimation.Components;
    using Components;
    using Core.Components;
    using Equip.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
    public class AbilityInventoryAspect : EcsAspect
    {
        public EcsPool<AbilityIdComponent> Id;
        public EcsPool<AbilityEquipComponent> AbilityEquip;
        public EcsPool<AbilityBuildingComponent> Building;
        public EcsPool<AbilityBlockedComponent> Blocked;
        public EcsPool<AbilityVisualComponent> Visual;
        public EcsPool<AbilitySlotComponent> Slot;
        public EcsPool<UserInputAbilityComponent> Input;
        public EcsPool<OwnerComponent> Owner;
        public EcsPool<OwnerLinkComponent> OwnerLink;
        public EcsPool<AbilityActiveAnimationComponent> Animation;
        public EcsPool<AbilityMetaLinkComponent> MetaLink;
        public EcsPool<AbilityConfigurationComponent> Configuration;
        public EcsPool<AbilityBuildingProcessingComponent> Processing;
        public EcsPool<AbilityLoadingComponent> Loading;
        public EcsPool<AbilityInventoryCompleteComponent> Complete;
        public EcsPool<AbilityValidationFailedComponent> Failed;
        public EcsPool<AbilityInventoryProfileComponent> ProfileTarget;

        //requests
        public EcsPool<EquipAbilityIdSelfRequest> EquipById;
        public EcsPool<EquipAbilityIdToChampionRequest> EquipToChampion;
        public EcsPool<EquipAbilityNameSelfRequest> EquipByName;
        public EcsPool<EquipAbilityReferenceSelfRequest> EquipByReference;
        public EcsPool<EquipAbilitySelfRequest> Equip;
        public EcsPool<LoadAbilityMetaRequest> LoadMeta;
        
        //event
        public EcsPool<AbilityEquipChangedEvent> EquipChanged;
    }
}