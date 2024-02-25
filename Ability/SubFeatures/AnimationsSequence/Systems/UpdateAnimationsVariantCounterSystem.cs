namespace Game.Ecs.Ability.SubFeatures.CriticalAnimations.Systems
{
    using System;
    using Common.Components;
    using Components;
    using Core.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// increase counter on each animation
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class UpdateAnimationsVariantCounterSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;

        private EcsPool<AbilityAnimationVariantCounterComponent> _counterPool;
        private EcsPool<AbilityAnimationVariantsComponent> _variantsPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<AbilityStartUsingSelfEvent>()
                .Inc<OwnerComponent>()
                .Inc<AbilityAnimationVariantCounterComponent>()
                .Inc<AbilityAnimationVariantsComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var abilityEntity in _filter)
            {
                ref var counterComponent = ref _counterPool.Get(abilityEntity);
                ref var variantsComponent = ref _variantsPool.Get(abilityEntity);

                var animationsCount = variantsComponent.Variants.Count;
                if(animationsCount == 0)
                    continue;
                
                var counter = counterComponent.Value;
                counter++;
                counter = counter % animationsCount;
                counterComponent.Value = counter;
            }
        }
    }
}