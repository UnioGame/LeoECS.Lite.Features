namespace Game.Ecs.Core.Death.Converters
{
    using System;
    using System.Threading;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UnityEngine;
    using UnityEngine.Playables;

    [Serializable]
    public class DeathAnimationComponentConverter : EcsComponentConverter
    {
        [SerializeField]
        public PlayableAsset deadAnimation;

        public override void Apply(EcsWorld world, int entity)
        {
            if (deadAnimation == null) return;
            
            var deathAnimationPool = world.GetPool<DeathAnimationComponent>();
            ref var deathAnimation = ref deathAnimationPool.Add(entity);
            deathAnimation.Animation = deadAnimation;
        }
    }
}