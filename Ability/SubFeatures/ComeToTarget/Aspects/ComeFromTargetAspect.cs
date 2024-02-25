namespace Game.Ecs.Ability.SubFeatures.ComeToTarget.Aspects
{
    using System;
    using Characteristics.Radius.Component;
    using Components;
    using Core.Components;
    using Core.Death.Components;
    using Leopotam.EcsLite;
    using Movement.Components;
    using Target.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    using UniGame.LeoEcs.Shared.Components;

    [Serializable]
    public class ComeFromTargetAspect : EcsAspect
    {
        public EcsPool<UpdateComePointComponent> Update;
        public EcsPool<AbilityTargetsComponent> Targets;
        public EcsPool<TransformComponent> Transform;
        public EcsPool<TransformPositionComponent> Position;
        public EcsPool<OwnerComponent> Owner;
        public EcsPool<RadiusComponent> Radius;
        public EcsPool<EntityAvatarComponent> Avatar;
        public EcsPool<DeferredAbilityComponent> Deferred;
        public EcsPool<ComePointComponent> Point;
        public EcsPool<RevokeComeToEndOfRequest> Revoke;
        public EcsPool<DestroyComponent> Dead;
    }
}