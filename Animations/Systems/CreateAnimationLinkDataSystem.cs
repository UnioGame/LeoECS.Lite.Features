namespace Game.Ecs.Animations.Systems
{
    using System;
    using Aspects;
    using Components.Requests;
    using Data;
    using Leopotam.EcsLite;
    using UniGame.Core.Runtime;
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
    public sealed class CreateAnimationLinkDataSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private ILifeTime _worldLifeTime;
        private AnimationToolSystem _animationTool;
        
        private AnimationTimelineAspect _animationAspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _worldLifeTime = _world.GetWorldLifeTime();
            _animationTool = _world.GetGlobal<AnimationToolSystem>();
            
            _filter = _world
                .Filter<CreateAnimationLinkSelfRequest>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var animationEntity in _filter)
            {
                ref var request = ref _animationAspect.CreateLinkSelfAnimation.Get(animationEntity);
                ref var selfRequest = ref _animationAspect.CreateSelfAnimation.GetOrAddComponent(animationEntity);
                
                var animationLink = request.Data;
                if(animationLink == null) continue;
                
                selfRequest.Target = request.Target;
                selfRequest.Owner = request.Owner;
                
                var playableAsset = animationLink.animation;
                var playableAssetDuration = playableAsset == null || animationLink.duration > 0
                    ? animationLink.duration
                    : (float)playableAsset.duration;
                
                selfRequest.BindingData = animationLink.bindingData;
                selfRequest.Animation = playableAsset;
                selfRequest.WrapMode = animationLink.wrapMode;
                selfRequest.Duration = playableAssetDuration;
                selfRequest.Speed = animationLink.animationSpeed;
            }
        }
    }
}