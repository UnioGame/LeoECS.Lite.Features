namespace Game.Ecs.Animations.Systems
{
    using System;
    using Aspects;
    using Characteristics.Cooldown.Components;
    using Characteristics.Duration.Components;
    using Components;
    using Components.Requests;
    using Core.Components;
    using Data;
    using Leopotam.EcsLite;
    using Time.Service;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    using UnityEngine.Playables;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class EvaluateAnimationSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private AnimationToolSystem _animationTool;
        
        private AnimationTimelineAspect _animationAspect;
        private PlayableAnimatorAspect _animatorAspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _animationTool = _world.GetGlobal<AnimationToolSystem>();
            
            _filter = _world
                .Filter<AnimatorPlayingComponent>()
                .Inc<PlayableDirectorComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var animationEntity in _filter)
            {
                ref var activeAnimationComponent = ref _animatorAspect.PlayingTarget.Get(animationEntity);
                ref var directorComponent = ref _animatorAspect.Director.Get(animationEntity);
                var playableDirector = directorComponent.Value;
                
                if(!activeAnimationComponent.Value.Unpack(_world,out var playingEntity))
                    continue;

                ref var animationDuration = ref _animationAspect.Duration.Get(playingEntity);
                
                var state = playableDirector.state;
                var playable = playableDirector.playableAsset;
                var currentTime = playableDirector.time;
                var duration = animationDuration.Value;
                
                var isPlaying = playable != null && duration > currentTime;
                if (isPlaying) continue;
                
                ref var completeRequest = ref _animationAspect.StopSelf.GetOrAddComponent(animationEntity);
            }
        }
    }
}