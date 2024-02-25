namespace Game.Ecs.Ability.SubFeatures.Target.Converters
{
    using System.Threading;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Converter.Runtime.Abstract;
    using UnityEngine;

    public sealed class LookAtWhenAbilityApplyMonoConverter : MonoLeoEcsConverter
    {
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            var canRotatePool = world.GetPool<CanLookAtComponent>();
            canRotatePool.Add(entity);
        }
    }
}