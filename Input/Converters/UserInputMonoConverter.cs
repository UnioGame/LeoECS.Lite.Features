using UniCore.Runtime.ProfilerTools;

namespace Game.Ecs.Input.Converters
{
    using System.Threading;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UnityEngine;

    public sealed class UserInputMonoConverter : MonoLeoEcsConverter
    {
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            var inputTargetPool = world.GetPool<UserInputTargetComponent>();
            inputTargetPool.Add(entity);
            
            GameLog.Log($"PLAYER ID {entity}",Color.green);
        }
    }
}