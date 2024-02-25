namespace Game.Ecs.Ability.UserInput
{
    using System;
    using Systems;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Tools;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UnityEngine;

    [Serializable]
    [CreateAssetMenu(menuName = "Game/Feature/Ability/Ability Input Feature", 
        fileName = "Ability Input Feature")]
    public sealed class AbilityUserInputFeature : BaseLeoEcsFeature
    {
        public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new ProcessAbilityUpInputSystem());
            ecsSystems.Add(new RestoreDefaultInHandAbilitySystem());
            ecsSystems.Add(new ClearActiveTimeSystem());

            return UniTask.CompletedTask;
        }
    }
}