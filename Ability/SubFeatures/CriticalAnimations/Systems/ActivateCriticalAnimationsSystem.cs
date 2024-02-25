namespace Game.Ecs.Ability.SubFeatures.CriticalAnimations.Systems
{
    using System;
    using AbilityAnimation.Components;
    using Aspects;
    using Common.Components;
    using Components;
    using Core.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// if critical hit exist - add critical animations
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ActivateCriticalAnimationsSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsFilter _criticalFilter;
        private CriticalAnimationsAspect _aspect;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world
                .Filter<AbilityStartUsingSelfEvent>()
                .Inc<OwnerComponent>()
                .Inc<AbilityInHandComponent>()
                .Inc<AbilityCriticalAnimationTargetComponent>()
                .End();
            
            _criticalFilter = _world
                .Filter<AnimationDataLinkComponent>()
                .Inc<AbilityCriticalAnimationComponent>()
                .Exc<AbilityAnimationOptionComponent>()
                .Inc<OwnerComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var ownerComponent = ref _aspect.Owner.Get(entity);
                if(!ownerComponent.Value.Unpack(_world,out var owner))
                    continue;
                
                var hasCriticalAttackMarker = _aspect.CriticalAttackMarker.Has(owner);
                if(!hasCriticalAttackMarker) continue;
                
                foreach (var optionEntity in _criticalFilter)
                {
                    ref var optionOwnerComponent = ref _aspect.Owner.Get(optionEntity);
                    
                    if(!optionOwnerComponent.Value.Unpack(_world,out var optionOwner))
                        continue;
                    
                    if(optionOwner != entity) continue;
                    
                    _aspect.AbilityAnimationOption.GetOrAddComponent(optionEntity);
                }
            }
        }
    }
}