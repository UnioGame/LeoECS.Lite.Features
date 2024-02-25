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
    /// select ability animation options and apply to ability
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class AbilityActivateAnimationOptionsSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsFilter _abilityOptionsFilter;
        private AbilityAnimationAspect _animationAspect;
        private AbilityAnimationOptionAspect _optionAspect;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<AbilityStartUsingSelfEvent>()
                .Inc<OwnerComponent>()
                .Inc<AbilityActiveAnimationComponent>()
                .Inc<AbilityInHandComponent>()
                .End();

            _abilityOptionsFilter = _world
                .Filter<AbilityAnimationOptionComponent>()
                .Inc<AnimationDataLinkComponent>()
                .Inc<LinkToAnimationComponent>()
                .Inc<OwnerComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var activeAnimationComponent = ref _animationAspect.ActiveAnimation.Get(entity);
                
                foreach (var optionEntity in _abilityOptionsFilter)
                {
                    ref var optionOwnerComponent = ref _animationAspect.Owner.Get(optionEntity);
                    ref var linkToAnimationComponent = ref _animationAspect.AnimationLink.Get(optionEntity);
                    
                    if(!optionOwnerComponent.Value.Unpack(_world,out var optionOwner))
                        continue;
                    
                    if(optionOwner != entity) continue;

                    activeAnimationComponent.Value = linkToAnimationComponent.Value;
                    
                    break;
                }
            }
        }
    }
}