namespace Game.Ecs.GameEffects.HealingEffect
{
    using System;
    using Components;
    using Effects;
    using Leopotam.EcsLite;
    using UnityEngine;
    using UnityEngine.Serialization;

    [Serializable]
    public sealed class HealingEffectConfiguration : EffectConfiguration
    {
        [FormerlySerializedAs("_healingValue")]
        [SerializeField]
        [Min(0.0f)]
        public float healingValue;
        
        protected override void Compose(EcsWorld world, int effectEntity)
        {
            var healingPool = world.GetPool<HealingEffectComponent>();
            ref var healing = ref healingPool.Add(effectEntity);
            healing.Value = healingValue;
        }
    }
}