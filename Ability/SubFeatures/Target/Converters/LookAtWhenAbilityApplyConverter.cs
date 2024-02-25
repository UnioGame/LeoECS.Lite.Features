namespace Game.Ecs.Ability.SubFeatures.Target.Converters
{
    using System;
    using System.Threading;
    using Components;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime;
    using UnityEngine;

    [Serializable]
    public sealed class LookAtWhenAbilityApplyConverter : LeoEcsConverter
    {
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            var canRotatePool = world.GetPool<CanLookAtComponent>();
            canRotatePool.Add(entity);
        }
    }
}