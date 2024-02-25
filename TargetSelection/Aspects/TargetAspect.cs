namespace Game.Ecs.TargetSelection.Aspects
{
    using System;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
    public class TargetAspect : EcsAspect
    {
        public EcsPool<KDTreeDataComponent> Data;
        public EcsPool<KDTreeComponent> Tree;
        public EcsPool<KDTreeQueryComponent> Query;
        public EcsPool<TransformComponent> Transform;
        public EcsPool<TransformPositionComponent> Position;
    }
}