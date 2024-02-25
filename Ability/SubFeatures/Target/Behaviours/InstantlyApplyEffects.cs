namespace Game.Ecs.Ability.SubFeatures.Target.Behaviours
{
    using System;
    using Code.Configuration.Runtime.Ability.Description;
    using Components;
    using Leopotam.EcsLite;
    using UnityEngine;

    [Serializable]
    public sealed class InstantlyApplyEffects : IAbilityBehaviour
    {
        public void Compose(EcsWorld world, int abilityEntity, bool isDefault)
        {
            var instantlyPool = world.GetPool<CanInstantlyApplyEffects>();
            instantlyPool.Add(abilityEntity);
        }
    }
}