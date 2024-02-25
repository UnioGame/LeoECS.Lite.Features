namespace Game.Ecs.Ability.SubFeatures.CriticalAnimations.Behaviours
{
    using System;
    using System.Collections.Generic;
    using Ability.Components;
    using Abstract;
    using Components;
    using Core.Components;
    using Cysharp.Threading.Tasks;
    using Game.Code.Configuration.Runtime.Ability.Description;
    using Leopotam.EcsLite;
    using Tools;
    using UniGame.AddressableTools.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    /// <summary>
    /// create animation sequence of animations
    /// </summary>
    [Serializable]
    public class AnimationsSequenceBehavior : IAbilityBehaviour
    {
        public List<AssetReferenceAnimationsSequence> animations = new List<AssetReferenceAnimationsSequence>();

        [SerializeReference]
        public IAbilityAnimationBehavior SequenceBehavior = new LinearAnimationSelectionBehavior();
        
        [NonSerialized]
        private AbilityTools _tools;
        
        public void Compose(EcsWorld world, int abilityEntity, bool isDefault)
        {
            _tools = world.GetGlobal<AbilityTools>();

            ref var counterComponent = ref world.GetOrAddComponent<AbilityAnimationVariantCounterComponent>(abilityEntity);
            ref var variantsComponent = ref world.GetOrAddComponent<AbilityAnimationVariantsComponent>(abilityEntity);
            ref var abilityOwnerComponent = ref world.GetComponent<OwnerComponent>(abilityEntity);

            counterComponent.Value = 0;

            SequenceBehavior.Compose(world, abilityEntity, isDefault);
            
            foreach (var animation in animations)
            {
                var animationEntity = world.NewEntity();
                var packedAnimationEntity = world.PackEntity(animationEntity);
                ref var ownerComponent = ref world.AddComponent<OwnerComponent>(animationEntity);
                ref var abilityAnimationComponent = ref world.AddComponent<AbilityAnimationComponent>(animationEntity);
                ref var animationVariantComponent = ref world.AddComponent<AbilityAnimationVariantComponent>(animationEntity);

                var ability = world.PackEntity(abilityEntity);
                ownerComponent.Value = ability;
                abilityAnimationComponent.Ability = ability;
    
                //build animation with milestones
                _tools.ComposeAbilityAnimationAsync(world,
                        abilityOwnerComponent.Value,
                        packedAnimationEntity,
                        animation).Forget();
                
                variantsComponent.Variants.Add(packedAnimationEntity);
            }
        }
    }
}