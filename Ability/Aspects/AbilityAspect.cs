namespace Game.Ecs.Ability.Aspects
{
    using System;
    using AbilityInventory.Components;
    using Animations.Components.Requests;
    using Characteristics.Attack.Components;
    using Characteristics.Cooldown.Components;
    using Characteristics.Duration.Components;
    using Characteristics.Radius.Component;
    using Common.Components;
    using Components;
    using Components.Requests;
    using Core.Components;
    using GameLayers.Category.Components;
    using GameLayers.Relationship.Components;
    using global::Ability.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Timer.Components;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
    public class AbilityAspect : EcsAspect
    {
        public EcsPool<OwnerComponent> Owner;
        public EcsPool<ActiveAbilityComponent> Active;
        public EcsPool<DefaultAbilityComponent> Default;
        public EcsPool<UserInputAbilityComponent> Input;
        public EcsPool<AbilityIdComponent> AbilityId;
        public EcsPool<AbilitySlotComponent> AbilitySlot;
        public EcsPool<NameComponent> Name;
        public EcsPool<IconComponent> Icon;
        public EcsPool<CategoryIdComponent> Category;
        public EcsPool<DurationComponent> Duration;
        public EcsPool<AnimationDataLinkComponent> AnimationLink;
        public EcsPool<RelationshipIdComponent> Relationship;
        public EcsPool<BaseCooldownComponent> BaseCooldown;
        public EcsPool<CooldownComponent> Cooldown;
        public EcsPool<CooldownStateComponent> CooldownState;
        public EcsPool<RadiusComponent> Radius;
        public EcsPool<DescriptionComponent> Description;
        public EcsPool<AbilityConfigurationComponent> Configuration;
        public EcsPool<AbilityEffectMilestonesComponent> EffectMilestones;
        public EcsPool<AbilityPauseComponent> Pause;
        public EcsPool<EntityAvatarComponent> Avatar;
        public EcsPool<AbilityMapComponent> AbilityMap;
        public EcsPool<AbilityEquippedComponent> AbilityEquipped;
        
        public EcsPool<AbilityEvaluationComponent> Evaluate;
        
        //is ability in use
        public EcsPool<AbilityUsingComponent> AbilityUsing;
        
        //requests
        
        public EcsPool<ActivateAbilityRequest> ActivateAbilityOnTarget;
        //complete ability
        public EcsPool<CompleteAbilitySelfRequest> CompleteAbility;
        
        public EcsPool<AbilityValidationSelfRequest> Validate;
        public EcsPool<SetAbilityBaseCooldownSelfRequest> SetBaseCooldown;
        public EcsPool<RecalculateCooldownSelfRequest> RecalculateCooldown;
        
        //activate ability
        public EcsPool<ActivateAbilitySelfRequest> ActivateAbility;
        public EcsPool<ApplyAbilityRadiusRangeRequest> ApplyRadiusRange;
        
        //events
        public EcsPool<AbilityStartUsingSelfEvent> UsingEvent;
        public EcsPool<AbilityCompleteSelfEvent> CompleteEvent;
        
        public EcsPool<EquipAbilityIdSelfRequest> EquipAbilityIdRequest;
        
        //animation
        public EcsPool<AbilityAwaitAnimationTriggerComponent> AwaitAnimationTrigger;
        public EcsPool<AnimationTriggerRequest> AnimationTrigger;
    }
}