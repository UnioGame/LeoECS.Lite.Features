namespace Ability.SubFeatures.AbilityAdjustableCooldown
{
    using System;
    using Cysharp.Threading.Tasks;
    using Game.Ecs.Ability.SubFeatures;
    using Leopotam.EcsLite;
    using Systems;
    using UnityEngine;

    [Serializable]
    [CreateAssetMenu(menuName = "Game/Feature/Ability/Ability Adjustable Cooldown SubFeature",fileName = "Ability Adjustable Cooldown SubFeature")]
    public class AbilityAdjustableCooldownSubFeature : AbilitySubFeature
    {
        public override UniTask<IEcsSystems> OnLastAbilitySystems(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new UpdateAbilityCooldownSystem());
            return base.OnActivateSystems(ecsSystems);
        }
    }
}