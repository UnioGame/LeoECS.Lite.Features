namespace Game.Ecs.Effects.Data
{
    using System;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
    public class EffectTargetAspect : EcsAspect
    {
        public EcsPool<EffectRootTargetComponent> Target;
        public EcsPool<EffectParentComponent> Transform;
        public EcsPool<EffectRootIdComponent> Id;
    }
}