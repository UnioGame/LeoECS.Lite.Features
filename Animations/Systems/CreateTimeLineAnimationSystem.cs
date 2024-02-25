namespace Game.Ecs.Animations.Systems
{
    using System;
    using Aspects;
    using Components.Requests;
    using Data;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class CreateTimeLineAnimationSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private AnimationToolSystem _animationTool;
        
        private AnimationTimelineAspect _animationAspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _animationTool = _world.GetGlobal<AnimationToolSystem>();
            
            _filter = _world
                .Filter<CreateAnimationPlayableSelfRequest>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var animationEntity in _filter)
            {
                ref var request = ref _animationAspect.CreateSelfAnimation.Get(animationEntity);
                
                ref var durationComponent = ref _animationAspect.Duration.GetOrAddComponent(animationEntity);
                ref var animationComponent = ref _animationAspect.Animation.GetOrAddComponent(animationEntity);
                ref var bindingDataComponent = ref _animationAspect.Binding.GetOrAddComponent(animationEntity);
                ref var ownerComponent = ref _animationAspect.Owner.GetOrAddComponent(animationEntity);
                ref var targetComponent = ref _animationAspect.Target.GetOrAddComponent(animationEntity);
                ref var activeComponent = ref _animationAspect.Ready.GetOrAddComponent(animationEntity);
                ref var wrapModeComponent = ref _animationAspect.WrapMode.GetOrAddComponent(animationEntity);
                ref var speedComponent = ref _animationAspect.Speed.GetOrAddComponent(animationEntity);
                ref var startTimeComponent = ref _animationAspect.StartTime.GetOrAddComponent(animationEntity);

                speedComponent.Value = request.Speed > 0 ? request.Speed : speedComponent.Value;
                targetComponent.Value = request.Target;
                ownerComponent.Value = request.Owner;
                bindingDataComponent.Value = request.BindingData;
                animationComponent.Value = request.Animation;
                wrapModeComponent.Value = request.WrapMode;
                durationComponent.Value = request.Duration;
                
                var playable = animationComponent.Value;

                durationComponent.Value = durationComponent.Value <= 0
                    ? playable == null ? 0f : (float)playable.duration
                    : durationComponent.Value;
            }
        }
    }
}