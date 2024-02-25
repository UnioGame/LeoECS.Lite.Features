namespace Game.Ecs.Ability.SubFeatures.ComeToTarget
{
    using System;
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;
    using Systems;
    using Leopotam.EcsLite;
    using UnityEngine;
    using UserInput.Systems;

    [Serializable]
    [CreateAssetMenu(menuName = "Game/Feature/Ability/ComeToTarget SubFeature",fileName = "ComeToTarget SubFeature")]
    public sealed class ComeToTargetSubFeature : AbilitySubFeature
    {
        public override UniTask<IEcsSystems> OnUtilitySystems(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new ComeToTargetByUserInputSystem());
            ecsSystems.Add(new UpdateComePointFromTargetSystem());
            ecsSystems.Add(new RevokeUpdateComePointByMovementSystem());
            ecsSystems.Add(new RevokeUpdateComePointByAbilityInHandSystem());
            ecsSystems.Add(new ApplyDeferredAbilitySystem());
            return UniTask.FromResult(ecsSystems);
        }
    }
}