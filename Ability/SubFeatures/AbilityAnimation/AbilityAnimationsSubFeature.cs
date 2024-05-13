namespace Game.Ecs.Ability.SubFeatures.AbilityAnimation
{
    using System;
    using Cysharp.Threading.Tasks;
    using Data;
    using global::Ability.SubFeatures.AbilityAnimation.Systems;
    using global::Ability.Systems;
    using Leopotam.EcsLite;
    using Systems;
    using UnityEngine;

    /// <summary>
    /// add critical animations if critical hit exist
    /// </summary>
    [Serializable]
    [CreateAssetMenu(menuName = "Game/Feature/Ability/AbilityAnimations SubFeature",fileName = "AbilityAnimations SubFeature")]
    public class AbilityAnimationsSubFeature : AbilitySubFeature
    {
        public AnimatorParametersMap animatorParametersMap;
        public override UniTask<IEcsSystems> OnCompleteAbilitySystems(IEcsSystems ecsSystems)
        {
            //clear ability animation when ability complete
            ecsSystems.Add(new ClearAbilityAnimationSystem());
            return UniTask.FromResult(ecsSystems);
        }
        
        public override UniTask<IEcsSystems> OnActivateSystems(IEcsSystems ecsSystems)
        {
            //reset ability animation to default when ability activated
            ecsSystems.Add(new AbilityResetDefaultAnimationOnActivateSystem());
            //select and activate animation options
            ecsSystems.Add(new AbilityActivateAnimationOptionsSystem());
            //reset all ability options when ability activated
            ecsSystems.Add(new AbilityResetAnimationOptionsSystem());
            
            //trigger animation trough animator component
            ecsSystems.Add(new AbilityTriggerAnimatorSystem());
            //update attack animation speeed
            ecsSystems.Add(new UpdateAttackAnimationSpeedSystem(animatorParametersMap));
            //update movement animation speed
            // ecsSystems.Add(new UpdateMovementAnimationSpeedSystem(animatorParametersMap));
            return UniTask.FromResult(ecsSystems);
        }
        
        public override UniTask<IEcsSystems> OnEvaluateAbilitySystem(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new EvaluateAbilityAnimationSystem());
            return UniTask.FromResult(ecsSystems);
        }

        public override UniTask<IEcsSystems> OnPreparationApplyEffectsSystems(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new AbilityAwaitAnimationTriggerSystem());
            return base.OnPreparationApplyEffectsSystems(ecsSystems);
        }
    }
}