namespace Game.Ecs.Ability.SubFeatures.CriticalAnimations
{
    using System;
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Systems;
    using UnityEngine;

    /// <summary>
    /// add critical animations if critical hit exist
    /// </summary>
    [Serializable]
    [CreateAssetMenu(menuName = "Game/Feature/Ability/Ability Animations Options SubFeature",fileName = "Ability Animations Options SubFeature")]
    public class AbilityAnimationsSequenceSubFeature : AbilitySubFeature
    {
        public override UniTask<IEcsSystems> OnActivateSystems(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new SelectLinearAnimationVariantSystem());
            ecsSystems.Add(new UpdateAnimationsVariantCounterSystem());

            return UniTask.FromResult(ecsSystems);
        }
    }
}