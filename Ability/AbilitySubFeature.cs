namespace Game.Ecs.Ability.SubFeatures
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using UnityEngine;

    [Serializable]
    public abstract class AbilitySubFeature : ScriptableObject
    {
        public virtual UniTask<IEcsSystems> OnInitializeSystems(IEcsSystems ecsSystems)
        {
            return UniTask.FromResult(ecsSystems);
        }
        
        public virtual UniTask<IEcsSystems> OnStartSystems(IEcsSystems ecsSystems)
        {
            return UniTask.FromResult(ecsSystems);
        }

        public virtual UniTask<IEcsSystems> OnAfterInHandSystems(IEcsSystems ecsSystems)
        {
            return UniTask.FromResult(ecsSystems);
        }
        
        
        public virtual UniTask<IEcsSystems> OnCompleteAbilitySystems(IEcsSystems ecsSystems)
        {
            return UniTask.FromResult(ecsSystems);
        }
        
        public virtual UniTask<IEcsSystems> OnBeforeApplyAbility(IEcsSystems ecsSystems)
        {
            return UniTask.FromResult(ecsSystems);
        }

        public virtual UniTask<IEcsSystems> OnRevokeSystems(IEcsSystems ecsSystems)
        {
            return UniTask.FromResult(ecsSystems);
        }

        public virtual UniTask<IEcsSystems> OnUtilitySystems(IEcsSystems ecsSystems)
        {
            return UniTask.FromResult(ecsSystems);
        }
        
        public virtual UniTask<IEcsSystems> OnActivateSystems(IEcsSystems ecsSystems)
        {
            return UniTask.FromResult(ecsSystems);
        }
        
        public virtual UniTask<IEcsSystems> OnEvaluateAbilitySystem(IEcsSystems ecsSystems)
        {
            return UniTask.FromResult(ecsSystems);
        }
        
        public virtual UniTask<IEcsSystems> OnPreparationApplyEffectsSystems(IEcsSystems ecsSystems)
        {
            return UniTask.FromResult(ecsSystems);
        }

        public virtual UniTask<IEcsSystems> OnApplyEffectsSystems(IEcsSystems ecsSystems)
        {
            return UniTask.FromResult(ecsSystems);
        }
        
        public virtual UniTask<IEcsSystems> OnLastAbilitySystems(IEcsSystems ecsSystems)
        {
            return UniTask.FromResult(ecsSystems);
        }
    }
}