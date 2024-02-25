namespace Game.Ecs.Animations.Systems
{
    using System;
    using Aspects;
    using Components;
    using Components.Requests;
    using Core.Components;
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
    public sealed class HandlePlayRequestAtPlayingDirectorSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private AnimationTimelineAspect _animationAspect;
        private PlayableAnimatorAspect _animatorAspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<PlayOnDirectorSelfRequest>()
                .Inc<AnimatorPlayingComponent>()
                .Inc<PlayableDirectorComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var animatorEntity in _filter)
            {
                _animatorAspect.StopSelf.GetOrAddComponent(animatorEntity);
            }
        }
    }
}