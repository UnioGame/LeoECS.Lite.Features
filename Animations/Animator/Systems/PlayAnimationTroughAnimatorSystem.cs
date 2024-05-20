namespace Animations.Animatror.Systems
{
    using System;
    using Animator.Data;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    /// <summary>
    /// Система проигрывает анимацию через компонент Animator сопоставляя AnimationClipId и имя анимационного стейта
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class PlayAnimationTroughAnimatorSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private AnimationsAnimatorAspect _aspect;
        private AnimatorsMap _animatorIdsMap;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<PlayAnimationByIdSelfRequest>()
                .Inc<AnimatorComponent>()
                .End();
            _animatorIdsMap = _world.GetGlobal<AnimatorsMap>();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var request = ref _aspect.PlaySelf.Get(entity);
                ref var animatorComponent = ref _aspect.Animator.Get(entity);
                var animationClipId = request.id;
                
                if (!_animatorIdsMap.data.TryGetValue(animationClipId, out var stateAndClipData))
                    continue;
#if UNITY_EDITOR
                if (!animatorComponent.Value.HasState(0, stateAndClipData.stateNameHash))
                {
                    Debug.LogError(
                        $"Animator {animatorComponent.Value.name} has no state {stateAndClipData.stateNameHash}");
                }
#endif
                animatorComponent.Value.Play(stateAndClipData.stateNameHash);
                ref var currentlyPlayingAnimationComponent = ref _aspect.CurrentlyPlaying.GetOrAddComponent(entity);
                currentlyPlayingAnimationComponent.Id = animationClipId;

                _aspect.PlaySelf.Del(entity);
            }
        }

    }
}