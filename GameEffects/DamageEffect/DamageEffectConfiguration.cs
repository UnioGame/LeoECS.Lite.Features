namespace Game.Ecs.GameEffects.DamageEffect
{
    using System;
    using Components;
    using DamageTypes;
    using DamageTypes.Abstracts;
    using Effects;
    using Leopotam.EcsLite;
    using UniCore.Runtime.ProfilerTools;
    using UnityEngine;

    [Serializable]
    public sealed class DamageEffectConfiguration : EffectConfiguration
    {
        #region Inspector

        [SerializeReference]
        public IDamageType DamageType = new PhysicsDamageType();

        #endregion
        
        [SerializeField]
        [Min(0.0f)]
        private float _damageValue;
        
        protected override void Compose(EcsWorld world, int effectEntity)
        {
            var damagePool = world.GetPool<DamageEffectComponent>();
            ref var damage = ref damagePool.Add(effectEntity);
            damage.Value = _damageValue;
            DamageType.Compose(world, effectEntity);
        }
    }
}