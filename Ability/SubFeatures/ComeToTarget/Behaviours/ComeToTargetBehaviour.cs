namespace Game.Ecs.Ability.SubFeatures.ComeToTarget.Behaviours
{
    using System;
    using Code.Configuration.Runtime.Ability.Description;
    using ComeToTarget.Components;
    using Leopotam.EcsLite;
    using UnityEngine;

    [Serializable]
    public sealed class ComeToTargetBehaviour : IAbilityBehaviour
    {
        public void Compose(EcsWorld world, int abilityEntity, bool isDefault)
        {
            var comeToPool = world.GetPool<CanComeToTargetComponent>();
            comeToPool.Add(abilityEntity);
        }
    }
}