namespace Game.Ecs.GameEffects.TeleportEffect
{
    using System;
    using Components;
    using Effects;
    using Leopotam.EcsLite;

    [Serializable]
    public sealed class TeleportEffectConfiguration : EffectConfiguration
    {
        protected override void Compose(EcsWorld world, int effectEntity)
        {
            var teleportPool = world.GetPool<TeleportEffectComponent>();
            teleportPool.Add(effectEntity);
        }
    }
}