namespace Game.Ecs.Ability.SubFeatures.CriticalAnimations
{
    using System;
    using System.Collections.Generic;
    using AbilitySequence.Tools;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Systems;
    using Target.Tools;
    using Tools;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    /// <summary>
    /// add critical animations if critical hit exist
    /// </summary>
    [Serializable]
    [CreateAssetMenu(menuName = "Game/Feature/Ability/AbilityCriticalAnimations SubFeature",fileName = "AbilityCriticalAnimations SubFeature")]
    public class AbilityCriticalAnimationsSubFeature : AbilitySubFeature
    {
        private AbilitySequenceTools _abilitySequenceTools;
        
        public override UniTask<IEcsSystems> OnInitializeSystems(IEcsSystems ecsSystems)
        {
            var world = ecsSystems.GetWorld();
            _abilitySequenceTools = world.GetGlobal<AbilitySequenceTools>();
            
            return base.OnInitializeSystems(ecsSystems);
        }

        public override UniTask<IEcsSystems> OnStartSystems(IEcsSystems ecsSystems)
        {
            //detect critical hit and add critical animation to choose
            ecsSystems.Add(new ActivateCriticalAbilitySystem());
            
            return UniTask.FromResult(ecsSystems);
        }
        
        public override UniTask<IEcsSystems> OnActivateSystems(IEcsSystems ecsSystems)
        {
            //detect critical hit and add critical animation to choose
            ecsSystems.Add(new ActivateCriticalAnimationsSystem());
            
            return UniTask.FromResult(ecsSystems);
        }
        
        
    }
}