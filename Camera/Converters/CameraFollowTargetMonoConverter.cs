namespace Game.Ecs.Camera.Converters
{
    using System.Threading;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UnityEngine;

    public sealed class CameraFollowTargetMonoConverter : MonoLeoEcsConverter
    {
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            var followTargetPool = world.GetPool<CameraFollowTargetComponent>();
            followTargetPool.Add(entity);
        }
    }
}