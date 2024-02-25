namespace Game.Ecs.GameEffects.ImmobilityEffect
{
    using System;
    using Components;
    using Effects;
    using Leopotam.EcsLite;

    [Serializable]
    public sealed class ImmobilityEffectConfiguration : EffectConfiguration
    {
        protected override void Compose(EcsWorld world, int effectEntity)
        {
            var immobilityPool = world.GetPool<ImmobilityEffectComponent>();
            immobilityPool.Add(effectEntity);
        }
    }
}