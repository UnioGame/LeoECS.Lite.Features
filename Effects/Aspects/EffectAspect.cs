namespace Game.Ecs.Effects.Aspects
{
    using System;
    using Characteristics.AbilityPower.Components;
    using Components;
    using Core.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

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
        public EcsPool<EffectPeriodicityComponent> Periodicity;
        public EcsPool<DelayedEffectComponent> Delayed;
        public EcsPool<CompletedDelayedEffectComponent> CompletedDelayed;
        
        
        //optional
        public EcsPool<EffectViewComponent> View;
        public EcsPool<EffectRootTransformsComponent> Transforms;
        public EcsPool<EntityAvatarComponent> Avatar;
        public EcsPool<EffectRootIdComponent> EffectRootId;
        public EcsPool<EffectShowCompleteComponent> ShowComplete;
        public EcsPool<TransformPositionComponent> Position;
        public EcsPool<TransformComponent> Transform;
        
        //Events
        public EcsPool<EffectAppliedSelfEvent> EffectAppliedSelfEvent;

        //requests
        public EcsPool<CreateEffectSelfRequest> Create;
        public EcsPool<DestroyEffectViewSelfRequest> DestroyView;
        public EcsPool<DestroyEffectSelfRequest> DestroyEffect;
        public EcsPool<ApplyEffectSelfRequest> Apply;
        
    }
}