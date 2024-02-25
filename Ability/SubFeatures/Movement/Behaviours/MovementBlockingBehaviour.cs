namespace Game.Ecs.Ability.SubFeatures.Movement.Behaviours
{
    using System;
    using Code.Configuration.Runtime.Ability.Description;
    using Components;
    using Leopotam.EcsLite;
    using UnityEngine;

    [Serializable]
    public sealed class MovementBlockingBehaviour : IAbilityBehaviour
    {
        public void Compose(EcsWorld world, int abilityEntity, bool isDefault)
        {
            var canBlockPool = world.GetPool<CanBlockMovementComponent>();
            canBlockPool.Add(abilityEntity);
        }

        public void DrawGizmos(GameObject target)
        {
        }
    }
}