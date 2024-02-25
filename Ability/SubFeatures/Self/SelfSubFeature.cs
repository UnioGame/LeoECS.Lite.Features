namespace Game.Ecs.Ability.SubFeatures.Self
{
    using System;
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;
    using Systems;
    using Leopotam.EcsLite;
    using UnityEngine;
    using UserInput;

    [Serializable]
    [CreateAssetMenu(menuName = "Game/Feature/Ability/Self SubFeature",fileName = "Self SubFeature")]
    public sealed class SelfSubFeature : AbilitySubFeature
    {
        public override UniTask<IEcsSystems> OnApplyEffectsSystems(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new SelfApplyAbilityEffectsSystem());
            return UniTask.FromResult(ecsSystems);
        }
    }
}