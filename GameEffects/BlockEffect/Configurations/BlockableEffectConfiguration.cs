namespace Game.Ecs.GameEffects.DamageEffect
{
    using System;
    using Characteristics.Dodge.Components;
    using Effects;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;

    [Serializable]
    public sealed class BlockableEffectConfiguration : EffectConfiguration
    {
        protected override void Compose(EcsWorld world, int effectEntity)
        {
            var blockableDamageComponent = world.GetOrAddComponent<BlockableDamageComponent>(effectEntity);
        }
    }
}