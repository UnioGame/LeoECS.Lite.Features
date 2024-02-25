namespace Game.Ecs.Effects.Aspects
{
    using System;
    using Characteristics.AbilityPower.Components;
    using Components;
    using Core.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    using UniGame.LeoEcs.Shared.Components;

    [Serializable]
    public class EffectAspect : EcsAspect
    {
        public EcsPool<EffectComponent> Effect;
        public EcsPool<EffectsListComponent> List;
        public EcsPool<OwnerComponent> Owner;
        public EcsPool<AbilityPowerComponent> Power;
        public EcsPool<EffectViewDataComponent> ViewData;
        public EcsPool<EffectDurationComponent> Duration;
        public EcsPool<EffectParentComponent> Parent;
        
        //optional
        public EcsPool<EffectViewComponent> View;
        public EcsPool<EffectRootTransformsComponent> Transforms;
        public EcsPool<EntityAvatarComponent> Avatar;
        public EcsPool<EffectRootIdComponent> EffectRootId;
        public EcsPool<EffectShowCompleteComponent> ShowComplete;
        public EcsPool<TransformComponent> Transform;
        
        //requests
        public EcsPool<CreateEffectSelfRequest> Create;
    }
}