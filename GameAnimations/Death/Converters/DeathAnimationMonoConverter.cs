namespace Game.Ecs.Core.Death.Converters
{
    using System.Threading;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UnityEngine;
    using UnityEngine.Playables;

    public sealed class DeathAnimationMonoConverter : MonoLeoEcsConverter
    {
        [SerializeField]
        public PlayableAsset deadAnimation;

        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            if (deadAnimation == null) return;
            
            var deathAnimationPool = world.GetPool<DeathAnimationComponent>();
            ref var deathAnimationComponent = ref deathAnimationPool.Add(entity);
            deathAnimationComponent.Animation = deadAnimation;
        }
    }
}