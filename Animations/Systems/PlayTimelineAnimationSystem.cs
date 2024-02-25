namespace Game.Ecs.Animations.Systems
{
    using System;
    using Aspects;
    using Characteristics.Cooldown.Components;
    using Characteristics.Duration.Components;
    using Code.Animations;
    using Components;
    using Components.Requests;
    using Core.Components;
    using Data;
    using Leopotam.EcsLite;
    using Time.Service;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UnityEngine.Playables;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class PlayTimelineAnimationSystem : IEcsRunSystem,IEcsInitSystem
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
                .Filter<PlayAnimationSelfRequest>()
                .Inc<AnimationTargetComponent>()
                .Inc<AnimationStartTimeComponent>()
                .Inc<DurationComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var animationEntity in _filter)
            {
                ref var request = ref _animationAspect.PlaySelf.Get(animationEntity);
                ref var durationComponent = ref _animationAspect.Duration.Get(animationEntity);
                ref var startTimeComponent = ref _animationAspect.StartTime.Get(animationEntity);
                ref var speedComponent = ref _animationAspect.Speed.Get(animationEntity);
                
                durationComponent.Value = request.Duration > 0 ? request.Duration : durationComponent.Value;
                startTimeComponent.Value = request.StartTime > 0 ? request.StartTime : startTimeComponent.Value;
                speedComponent.Value = request.Speed > 0 ? request.Speed : speedComponent.Value;
                
                ref var targetComponent = ref _animationAspect.Target.GetOrAddComponent(animationEntity);
                
                if(!targetComponent.Value.Unpack(_world,out var directorEntity)) continue;
                
                if(!_animatorAspect.Director.Has(directorEntity)) continue;
                
                ref var playRequest = ref _animatorAspect.Play.GetOrAddComponent(directorEntity);
                playRequest.Animation = _world.PackEntity(animationEntity);
            }
        }
    }
}