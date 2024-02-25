namespace Game.Ecs.AbilityAgent
{
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UnityEngine;

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
    [CreateAssetMenu(menuName = "Game/Feature/Ability/Ability Agent Feature", fileName = "Ability Agent Feature")]
    public class AbilityAgentFeature : BaseLeoEcsFeature
    {
        public override async UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new InitializeAbilityAgentSystem());
            ecsSystems.Add(new CreateAbilityAgentSystem());
        }
    }
}