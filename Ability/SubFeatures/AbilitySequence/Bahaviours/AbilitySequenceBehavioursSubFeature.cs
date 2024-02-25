namespace Game.Ecs.Ability.SubFeatures.AbilitySequence
{
    using System;
    using Bahaviours.Systems;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using UnityEngine;

    /// <summary>
    /// add critical animations if critical hit exist
    /// </summary>
    [Serializable]
    [CreateAssetMenu(menuName = "Game/Feature/Ability/Ability Sequence Behaviours SubFeature",fileName = "Ability Sequence Behaviours SubFeature")]
    public class AbilitySequenceBehavioursSubFeature : AbilitySubFeature
    {
        public override UniTask<IEcsSystems> OnActivateSystems(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new ActivateSequenceOnAbilityStartSystem());
            return UniTask.FromResult(ecsSystems);
        }

        public override UniTask<IEcsSystems> OnLastAbilitySystems(IEcsSystems ecsSystems)
        {
            return base.OnLastAbilitySystems(ecsSystems);
        }
    }
}