namespace Game.Ecs.Gameplay.Death.Systems
{
    using System;
    using System.Linq;
    using Aspects;
    using Characteristics.Health.Components;
    using Core.Components;
    using global::Animations.Animator.Data;
    using global::Animations.Animatror;
    using global::Animations.Animatror.Components;
    using Leopotam.EcsLite;
    using UniGame.Core.Runtime.Extension;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.Runtime.ObjectPool.Extensions;
    using UnityEngine;
    using UnityEngine.Pool;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class EvaluateDeathAnimationByAnimatorSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly AnimationClipId _deathAnimationClipId;
        private EcsWorld _world;
        private EcsFilter _filter;
        private DeathAspect _deathAspect;
        private EcsPool<AnimationIsLiveComponent> _animationIsLivePool;

        public EvaluateDeathAnimationByAnimatorSystem(AnimationClipId deathAnimationClipId)
        {
            _deathAnimationClipId = deathAnimationClipId;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<PrepareToDeathComponent>()
                .Inc<AnimatorComponent>()
                .Inc<AwaitDeathCompleteComponent>()
                .Inc<AnimationIsLiveComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var animator = ref _world.GetComponent<AnimatorComponent>(entity);
                //можно сломать. лучше брать ответ от аниматора
                if(animator.Value.IsPlaying()) continue;
                ref var clip = ref _animationIsLivePool.Get(entity);
                if(clip.Id != _deathAnimationClipId) continue;
                _deathAspect.Completed.Add(entity);
            }
        }
    }
}