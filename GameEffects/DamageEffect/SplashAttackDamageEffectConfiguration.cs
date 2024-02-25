namespace Game.Ecs.GameEffects.DamageEffect
{
    using System;
    using System.Collections.Generic;
    using Components;
    using Effects;
    using Leopotam.EcsLite;
    using UnityEngine;

    [Serializable]
    public sealed class SplashAttackDamageEffectConfiguration : EffectConfiguration
    {
        protected override void Compose(EcsWorld world, int effectEntity)
        {
            var damagePool = world.GetPool<SplashAttackDamageEffectComponent>();
            damagePool.Add(effectEntity);
        }
    }
}