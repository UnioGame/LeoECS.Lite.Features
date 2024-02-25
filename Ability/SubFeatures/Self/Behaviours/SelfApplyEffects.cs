namespace Game.Ecs.Ability.SubFeatures.Self.Behaviours
{
    using System;
    using Ability.UserInput.Components;
    using Code.Configuration.Runtime.Ability.Description;
    using Components;
    using Leopotam.EcsLite;
    using UnityEngine;

    [Serializable]
    public sealed class SelfApplyEffects : IAbilityBehaviour
    {
        public void Compose(EcsWorld world, int abilityEntity , bool isDefault)
        {
            var pressPool = world.GetPool<CanApplyWhenUpInputComponent>();
            if (!pressPool.Has(abilityEntity))
                pressPool.Add(abilityEntity);
        }
    }
}