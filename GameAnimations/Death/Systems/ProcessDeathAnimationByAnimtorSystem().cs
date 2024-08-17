namespace Game.Ecs.Gameplay.Death.Systems
{
    using System;
    using Aspects;
    using Characteristics.Health.Components;
    using Core.Components;
    using global::Animations.Animator.Data;
    using global::Animations.Animatror.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;

    /// <summary>
    /// Trigger death animation by animator
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ProcessDeathAnimationByAnimatorSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly AnimationClipId _deathAnimationClipId;
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<PlayAnimationByIdSelfRequest> _playSelfPool;
        private DeathAspect _deathAspect;
        public ProcessDeathAnimationByAnimatorSystem(AnimationClipId deathAnimationClipId)
        {
            _deathAnimationClipId = deathAnimationClipId;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<PrepareToDeathComponent>()
                .Inc<AnimatorComponent>()
                .Exc<AwaitDeathCompleteComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var animator = ref _world.GetComponent<AnimatorComponent>(entity);
                ref var playRequest = ref _playSelfPool.Add(entity);
                playRequest.id = _deathAnimationClipId;
                
                //pause death process and await actions
                _deathAspect.Disabled.TryAdd(entity);
                _deathAspect.AwaitDeath.Add(entity);
            }
        }
    }
}