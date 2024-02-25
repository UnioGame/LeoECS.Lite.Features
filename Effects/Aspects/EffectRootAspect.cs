namespace Game.Ecs.Effects.Aspects
{
    using System;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
    public class EffectRootAspect : EcsAspect
    {
        public EcsPool<EffectRootTransformsComponent> Target;
        public EcsPool<EffectRootIdComponent> Id;
        public EcsPool<TransformComponent> Transform;
        public EcsPool<TransformPositionComponent> Position;
    }
}