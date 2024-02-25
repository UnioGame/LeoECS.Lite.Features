namespace Game.Ecs.GameEffects.FreezeEffect
{
    using System;
    using Components;
    using Effects;
    using Leopotam.EcsLite;

    [Serializable]
    public sealed class FreezeEffectConfiguration : EffectConfiguration
    {
        protected override void Compose(EcsWorld world, int effectEntity)
        {
            var freezeEffectPool = world.GetPool<FreezeEffectComponent>();
            freezeEffectPool.Add(effectEntity);
        }
    }
}