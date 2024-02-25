namespace Game.Ecs.Ability.AbilityUtilityView.Converters
{
    using System;
    using System.Threading;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UnityEngine;

    [Serializable]
    public sealed class VisibleUtilityViewConverter : LeoEcsConverter
    {
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            var visiblePool = world.GetPool<VisibleUtilityViewComponent>();
            visiblePool.Add(entity);
        }
    }
}