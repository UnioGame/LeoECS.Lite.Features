namespace Game.Ecs.Effects.Aspects
{
    using System;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    using UniGame.LeoEcs.Shared.Components;

    [Serializable]
    public class EffectRootAspect : EcsAspect
    {
        public EcsPool<EffectRootTransformsComponent> Target;
        public EcsPool<EffectRootIdComponent> Id;
        public EcsPool<TransformComponent> Transform;
    }
}