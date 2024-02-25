namespace Game.Ecs.Ability.AbilityUtilityView.Converters
{
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UnityEngine;

    public sealed class VisibleUtilityViewMonoConverter : MonoLeoEcsConverter
    {
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            var visiblePool = world.GetPool<VisibleUtilityViewComponent>();
            visiblePool.Add(entity);
        }
    }
}