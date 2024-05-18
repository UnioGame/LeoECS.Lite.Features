namespace Game.Ecs.TargetSelection.Aspects
{
    using System;
    using Characteristics.Radius.Component;
    using Components;
    using Core.Components;
    using Core.Death.Components;
    using GameLayers.Category.Components;
    using GameLayers.Layer.Components;
    using GameLayers.Relationship.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
    public class TargetSelectionAspect : EcsAspect
    {
        public EcsPool<TargetsSelectionResultComponent> TargetSelectionResult;
        public EcsPool<TargetsSelectionRequestComponent> TargetSelectionRequest;
        public EcsPool<LayerIdComponent> Layer;
        public EcsPool<LayerOverrideComponent> LayerOverride;
        public EcsPool<CategoryIdComponent> Category;
        public EcsPool<TransformComponent> Transform;
        public EcsPool<TransformPositionComponent> Position;
        public EcsPool<EntityAvatarComponent> Avatar;
        public EcsPool<KDTreeDataComponent> KDData;
        public EcsPool<OwnerComponent> Owner;
        public EcsPool<DisabledComponent> Disabled;
        
        public EcsPool<RadiusComponent> Radius;
        public EcsPool<RelationshipIdComponent> Relationship;
    }
}