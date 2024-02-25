namespace Game.Ecs.Ability.SubFeatures.Target.Behaviours
{
    using System;
    using Code.Configuration.Runtime.Ability.Description;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;

    [Serializable]
    public sealed class AttackRangeBehaviour : IAbilityBehaviour
    {
        public void Compose(EcsWorld world, int abilityEntity, bool isDefault)
        {
            world.GetOrAddComponent<AttackRangeEffectComponent>(abilityEntity);
        }
    }
}