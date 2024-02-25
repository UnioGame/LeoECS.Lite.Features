namespace Game.Ecs.Effects.Aspects
{
    using System;
    using Components;
    using Core.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
    public class EffectViewAspect : EcsAspect
    {
        public EcsPool<EffectViewComponent> View;
        public EcsPool<OwnerComponent> Owner;
        public EcsPool<EffectParentComponent> Parent;
    }
}