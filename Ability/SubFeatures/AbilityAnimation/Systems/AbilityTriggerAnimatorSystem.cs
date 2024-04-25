namespace Game.Ecs.Ability.SubFeatures.AbilityAnimation.Systems
{
    using System;
    using Animation.Components;
    using Animation.Data;
    using Aspects;
    using Common.Components;
    using Core.Components;
    using global::Ability.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// Система создает реквест на триггер анимации
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class AbilityTriggerAnimatorSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private AbilityAnimationAspect _animationAspect;
        private EcsPool<PlayAnimationByIdSelfRequest> _animationRequestPool;
        
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<AbilityStartUsingSelfEvent>()
                .Inc<OwnerComponent>()
                .Inc<TriggeredAnimationIdComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var abilityEntity in _filter)
            {
                ref var triggeredAnimationIdComponent = ref _animationAspect.ClipId.Get(abilityEntity);
                var animationId = triggeredAnimationIdComponent.animationId ;
                
                ref var ownerComponent = ref _world.GetPool<OwnerComponent>().Get(abilityEntity);
                if(!ownerComponent.Value.Unpack(_world, out var ownerEntity)) continue;

                if(!_animationAspect.Animator.Has(ownerEntity)) continue;
                
                ref var playAnimationByIdSelfRequest = ref _animationRequestPool.Add(ownerEntity);
                playAnimationByIdSelfRequest.id = (AnimationClipId)animationId;
            }
        }
    }
}