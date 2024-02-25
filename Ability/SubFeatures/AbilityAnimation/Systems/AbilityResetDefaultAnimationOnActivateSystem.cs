namespace Game.Ecs.Ability.SubFeatures.AbilityAnimation.Systems
{
    using System;
    using Animations.Components;
    using Aspects;
    using Components;
    using Game.Ecs.Ability.Common.Components;
    using Game.Ecs.Core.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// reset ability animation to default state when ability activated
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class AbilityResetDefaultAnimationOnActivateSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        
        private AbilityAnimationAspect _animationAspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<AbilityStartUsingSelfEvent>()
                .Inc<OwnerComponent>()
                .Inc<AbilityInHandComponent>()
                .Inc<AnimationDataLinkComponent>()
                .Inc<AbilityActiveAnimationComponent>()
                .Inc<LinkToAnimationComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var activeAnimationComponent = ref _animationAspect.ActiveAnimation.Get(entity);
                ref var animationComponent = ref _animationAspect.AnimationLink.Get(entity);
                activeAnimationComponent.Value = animationComponent.Value;
            }
        }
    }
}