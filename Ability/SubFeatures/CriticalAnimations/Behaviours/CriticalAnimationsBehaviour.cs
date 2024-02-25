namespace Game.Ecs.Ability.SubFeatures.CriticalAnimations.Behaviours
{
    using System;
    using System.Collections.Generic;
    using Ability.Components;
    using Code.Animations;
    using Components;
    using Core.Components;
    using Cysharp.Threading.Tasks;
    using Game.Code.Configuration.Runtime.Ability.Description;
    using Leopotam.EcsLite;
    using Tools;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// create animations options for critical strike
    /// </summary>
    [Serializable]
    public class CriticalAnimationsBehaviour : IAbilityBehaviour
    {
        public List<AnimationLink> animations = new List<AnimationLink>();

        public void Compose(EcsWorld world, int abilityEntity, bool isDefault)
        {
            var tools = world.GetGlobal<AbilityTools>();
            world.GetOrAddComponent<AbilityCriticalAnimationTargetComponent>(abilityEntity);
            
            foreach (var animation in animations)
            {
                var animationEntity = world.NewEntity();
                var packedAnimationEntity = world.PackEntity(animationEntity);
                ref var ownerComponent = ref world.AddComponent<OwnerComponent>(animationEntity);
                ref var criticalComponent = ref world.AddComponent<AbilityCriticalAnimationComponent>(animationEntity);
                ref var abilityAnimationComponent = ref world.AddComponent<AbilityAnimationComponent>(animationEntity);

                var ability = world.PackEntity(abilityEntity);
                ownerComponent.Value = ability;
                abilityAnimationComponent.Ability = ability;

                //build animation with milestones
                tools.ComposeAbilityAnimation(world,
                    ref ownerComponent.Value,
                    ref packedAnimationEntity,
                    animation);
            }
        }
    }
}