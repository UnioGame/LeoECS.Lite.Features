namespace Game.Ecs.GameEffects.ShieldEffect
{
    using System;
    using Components;
    using Effects;
    using Leopotam.EcsLite;
    using UnityEngine;

    [Serializable]
    public sealed class ShieldEffectConfiguration : EffectConfiguration
    {
        [SerializeField]
        private float _shieldValue;
        
        protected override void Compose(EcsWorld world, int effectEntity)
        {
            var shieldPool = world.GetPool<ShieldEffectComponent>();
            ref var shield = ref shieldPool.Add(effectEntity);
            shield.MaxValue = _shieldValue;
        }
    }
}