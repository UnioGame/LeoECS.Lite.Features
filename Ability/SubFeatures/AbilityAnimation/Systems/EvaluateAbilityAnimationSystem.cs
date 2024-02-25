namespace Game.Ecs.Ability.SubFeatures.AbilityAnimation.Systems
{
    using System;
    using Animations.Aspects;
    using Aspects;
    using Code.Animations;
    using Components;
    using Game.Ecs.Ability.Common.Components;
    using Game.Ecs.Characteristics.Cooldown.Components;
    using Game.Ecs.Characteristics.Duration.Components;
    using Game.Ecs.Core.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Timer.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
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
    public sealed class EvaluateAbilityAnimationSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        private AbilityAnimationAspect _abilityAnimationAspect;
        private AnimationTimelineAspect _animationAspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<AbilityStartUsingSelfEvent>()
                .Inc<AbilityActiveAnimationComponent>()
                .Inc<CooldownComponent>()
                .Inc<OwnerComponent>()
                .Inc<DurationComponent>()
                .Inc<AbilityUsingComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var activeAnimationComponent = ref _abilityAnimationAspect.ActiveAnimation.Get(entity);
                ref var ownerComponent = ref _abilityAnimationAspect.Owner.Get(entity);
                ref var durationComponent = ref _abilityAnimationAspect.Duration.Get(entity);
                ref var cooldownComponent = ref _abilityAnimationAspect.Cooldown.Get(entity);
                
                if(!ownerComponent.Value.Unpack(_world, out var ownerEntity))
                    continue;
                
                if(!activeAnimationComponent.Value.Unpack(_world,out var animationEntity))
                    continue;
                
                ref var playAnimationRequest = ref _animationAspect.PlaySelf.Add(animationEntity);
                var cooldown = cooldownComponent.Value;
                var duration = durationComponent.Value;
                
                var speed = cooldown <= 0 ? 1 : duration / cooldown;
                speed = speed <= 0.1f ? 1f : speed;
                
                playAnimationRequest.Duration = durationComponent.Value;
                playAnimationRequest.Speed = speed;
            }
        }
    }
}