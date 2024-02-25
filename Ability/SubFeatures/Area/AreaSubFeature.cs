namespace Game.Ecs.Ability.SubFeatures.Area
{
    using System;
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;
    using Systems;
    using Leopotam.EcsLite;
    using UnityEngine;

    [Serializable]
    [CreateAssetMenu(menuName = "Game/Feature/Ability/AreaSubFeature",fileName = "AreaSubFeature")]
    public sealed class AreaSubFeature : AbilitySubFeature
    {
        public override UniTask<IEcsSystems> OnAfterInHandSystems(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new RemoveAreaForNonHandAbilitySystem());
            ecsSystems.Add(new SetupAreaByJoystickSystem());
            return UniTask.FromResult(ecsSystems);
        }

        public override UniTask<IEcsSystems> OnUtilitySystems(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new RotateToAreaSystem());
            return UniTask.FromResult(ecsSystems);
        }

        public override UniTask<IEcsSystems> OnPreparationApplyEffectsSystems(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new SelectTargetsForApplyEffectsSystem());
            return UniTask.FromResult(ecsSystems);
        }
    }
}