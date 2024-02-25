namespace Game.Ecs.Camera.Converters
{
    using System;
    using System.Threading;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UnityEngine;

    [Serializable]
    public sealed class CameraFollowTargetConverter : LeoEcsConverter
    {
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            var followTargetPool = world.GetPool<CameraFollowTargetComponent>();
            followTargetPool.Add(entity);
        }
    }
}