namespace Game.Code.Ai.ActivateAbility.Aspects
{
    using System;
    using Ecs.Ability.Common.Components;
    using Ecs.Characteristics.Radius.Component;
    using Ecs.Core.Components;
    using Ecs.GameAi.ActivateAbility;
    using Ecs.GameAi.ActivateAbility.Components;
    using Ecs.GameLayers.Category.Components;
    using Ecs.GameLayers.Layer.Components;
    using Ecs.GameLayers.Relationship.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
    public class AbilityAiActionAspect : EcsAspect
    {
        public EcsPool<RadiusComponent> Radius;
        public EcsPool<RelationshipIdComponent> Relationship;
        public EcsPool<CategoryIdComponent> Category;
        public EcsPool<TransformComponent> Transform;
        public EcsPool<TransformPositionComponent> Position;
        public EcsPool<TransformDirectionComponent> Direction;
        public EcsPool<AbilityActionActiveTargetComponent> ActiveTarget;
        public EcsPool<LayerIdComponent> LayerId;
        public EcsPool<EntityAvatarComponent> Avatar;
        public EcsPool<AbilityAiActionTargetComponent> AiTarget;
        public EcsPool<AbilityByDefaultComponent> DefaultAbility;
        public EcsPool<AbilityByRangeComponent> ByRangeAbility;
        public EcsPool<AbilityRangeActionDataComponent> AbilityRangeData;
        
        //request
        public EcsPool<ApplyAbilityBySlotSelfRequest> ApplyAbilityFromSlot;
    }
}