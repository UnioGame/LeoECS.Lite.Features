namespace Game.Ecs.Ability.SubFeatures.Area.Aspects
{
    using System;
    using Components;
    using Core.Components;
    using GameLayers.Category.Components;
    using GameLayers.Layer.Components;
    using GameLayers.Relationship.Components;
    using Leopotam.EcsLite;
    using Target.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    using UniGame.LeoEcs.Shared.Components;


#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class AreaAspect : EcsAspect
    {
        public EcsPool<AreaLocalPositionComponent> AreaPosition;
        public EcsPool<AreaRadiusComponent> AreaRadius;
        public EcsPool<OwnerComponent> Owner;
        public EcsPool<AbilityTargetsComponent> Targets;
        public EcsPool<RelationshipIdComponent> Relationship;
        public EcsPool<CategoryIdComponent> Category;
        public EcsPool<TransformPositionComponent> Position;
        public EcsPool<LayerIdComponent> Layer;
        public EcsPool<CanLookAtComponent> CanLookAtPool;
    }
}