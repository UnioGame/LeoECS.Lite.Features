namespace Game.Ecs.Effects.Data
{
    using System;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
    public class EffectGlobalAspect : EcsAspect
    {
        public EcsPool<EffectGlobalRootMarkerComponent> Global;
        public EcsPool<EffectRootTransformsComponent> Transforms;
        public EcsPool<EffectsRootDataComponent> Configuration;
    }
}