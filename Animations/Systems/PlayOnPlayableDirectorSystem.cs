namespace Game.Ecs.Animations.Systems
{
    using System;
    using Aspects;
    using Code.Animations;
    using Components.Requests;
    using Core.Components;
    using Leopotam.EcsLite;
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
    public sealed class PlayOnPlayableDirectorSystem : IEcsRunSystem,IEcsInitSystem
    {
        private float _minPlayableSpeed;
        
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private AnimationTimelineAspect _animationAspect;
        private PlayableAnimatorAspect _animatorAspect;

        public PlayOnPlayableDirectorSystem(float minPlayableSpeed)
        {
            _minPlayableSpeed = minPlayableSpeed;
        }
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<PlayOnDirectorSelfRequest>()
                .Inc<PlayableDirectorComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var animatorEntity in _filter)
            {
                ref var request = ref _animatorAspect.Play.Get(animatorEntity);
                
                if (!request.Animation.Unpack(_world, out var animationEntity))
                    continue;
                   
                ref var directorComponent = ref _animatorAspect.Director.Get(animatorEntity);
                
                ref var targetComponent = ref _animatorAspect.PlayingTarget.GetOrAddComponent(animatorEntity);
                
                targetComponent.Value = request.Animation;
                
                ref var animationComponent = ref _animationAspect.Animation.Get(animationEntity);
                ref var durationComponent = ref _animationAspect.Duration.Get(animationEntity);
                ref var wrapModeComponent = ref _animationAspect.WrapMode.Get(animationEntity);
                ref var bindingComponent = ref _animationAspect.Binding.Get(animationEntity);
                ref var startTimeComponent = ref _animationAspect.StartTime.Get(animationEntity);
                ref var speedComponent = ref _animationAspect.Speed.Get(animationEntity);
                
                var playableDirector = directorComponent.Value;
                var playable = animationComponent.Value;
                
                if(playable == null) continue;
                
                if (playableDirector.playableAsset == null || 
                    playableDirector.playableAsset != playable)
                {
                    playableDirector.playableAsset = playable;
                }
                
                if(bindingComponent.Value != null)
                    AnimationTool.ApplyBindings(playableDirector, bindingComponent.Value);
                
                //set animation start time
                playableDirector.time = startTimeComponent.Value;
                
                if (!playableDirector.playableGraph.IsValid())
                    playableDirector.RebuildGraph();
                   
                //calculate animation speed based on playable asset duration and given duration
                var duration = durationComponent.Value <= 0 ? playable.duration : durationComponent.Value;
                
                var speed = duration <= 0f ? 1f : (float)(playable.duration / duration);
                speed = speedComponent.Value > 0 ? speedComponent.Value : speed;
                speed = Mathf.Max(_minPlayableSpeed, speed);
                
                playableDirector.Play(playable,wrapModeComponent.Value);
                
                //set animation speed
                var rootPlayable = playableDirector.playableGraph.GetRootPlayable(0);
                rootPlayable.SetSpeed(speed);

                //mark animation as playing
                _animationAspect.Playing.GetOrAddComponent(animatorEntity);
                _animatorAspect.Playing.GetOrAddComponent(animatorEntity);

                //remove animation complete status
                _animationAspect.Complete.TryRemove(animatorEntity);
            }
        }
    }
}