namespace Game.Ecs.GameEffects.JumpForwardEffect
{
    using System;
    using Component;
    using Effects;
    using Effects.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [Serializable]
    public sealed class JumpForwardEffectConfiguration : EffectConfiguration
    {
        [SerializeField]
        [Min(0.0f)]
        private float _distance;
        protected override void Compose(EcsWorld world, int effectEntity)
        {
            ref var jumpForwardComponent = ref world.AddComponent<JumpForwardEffectComponent>(effectEntity);
            var durationPool = world.GetPool<EffectDurationComponent>();
            ref var durationComponent = ref durationPool.Get(effectEntity);
            jumpForwardComponent.Distance = _distance;
            jumpForwardComponent.Duration = durationComponent.Duration;
        }
    }
}