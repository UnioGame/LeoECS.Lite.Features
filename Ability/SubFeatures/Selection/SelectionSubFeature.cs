namespace Game.Ecs.Ability.SubFeatures.Selection
{
    using System;
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;
    using Systems;
    using Leopotam.EcsLite;
    using SubFeatures;
    using UnityEngine;
    using UserInput.Systems;

    [Serializable]
    [CreateAssetMenu(menuName = "Game/Feature/Ability/Selection SubFeature",fileName = "Selection SubFeature")]
    public sealed class SelectionSubFeature : AbilitySubFeature
    {
        public override UniTask<IEcsSystems> OnAfterInHandSystems(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new ClearSelectionForNonInHandAbilitySystem());
            ecsSystems.Add(new SelectTargetsSystem());
            return UniTask.FromResult(ecsSystems);
        }
    }
}