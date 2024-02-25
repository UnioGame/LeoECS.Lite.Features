namespace Game.Ecs.Ability.SubFeatures.CriticalAnimations
{
    using System;
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Systems;

    /// <summary>
    /// add critical animations if critical hit exist
    /// </summary>
    [Serializable]
    public class ApplyAbilitySubFeature : AbilitySubFeature
    {
        public override UniTask<IEcsSystems> OnActivateSystems(IEcsSystems ecsSystems)
        {
            return UniTask.FromResult(ecsSystems);
        }
    }
}