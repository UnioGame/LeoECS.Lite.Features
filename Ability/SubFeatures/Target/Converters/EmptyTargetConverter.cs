namespace Game.Ecs.Ability.SubFeatures.Target.Converters
{
    using System;
    using System.Threading;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [Serializable]
    public class EmptyTargetConverter : LeoEcsConverter
    {
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            world.AddComponent<EmptyTargetComponent>(entity);
        }
    }
}