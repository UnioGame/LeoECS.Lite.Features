namespace Game.Ecs.GameEffects.DamageEffect
{
    using System;
    using Components;
    using DamageTypes;
    using DamageTypes.Abstracts;
    using Effects;
    using Leopotam.EcsLite;
    using UnityEngine;

    [Serializable]
    public sealed class AttackDamageEffectConfiguration : EffectConfiguration
    {
        #region Inspector

        [SerializeReference]
        public IDamageType DamageType = new PhysicsDamageType();

        #endregion
        protected override void Compose(EcsWorld world, int effectEntity)
        {
            var damagePool = world.GetPool<AttackDamageEffectComponent>();
            damagePool.Add(effectEntity);
            DamageType.Compose(world, effectEntity);
        }
    }
}