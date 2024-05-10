namespace Game.Ecs.AI
{
    using Components;
    using Configurations;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.ExtendedSystems;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UnityEngine;
    
    [CreateAssetMenu(menuName = "Game/Feature/Ai Feature", fileName = "Ai Feature")]
    public sealed class AIFeature : BaseLeoEcsFeature
    {
        [SerializeField]
        private AiConfigurationAsset _aiConfigurationAsset;
        
        public override async UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            var configurationAsset = Instantiate(_aiConfigurationAsset);
            var configuration = configurationAsset.configuration;
            var actions = configuration.aiActions;

            // add all planners
            for (var index = 0; index < actions.Length; index++)
            {
                var aiActionData = actions[index];
                var planner = aiActionData.planner;
                await planner.Initialize(ecsSystems, aiActionData.actionId);
            }
            
            ecsSystems.Add(new AiPlanningSystem());
            ecsSystems.Add(new AiUpdatePlanningActionsStatusSystem(configuration.aiActions));
            foreach (var aiActionData in configuration.aiActions)
            {
                ecsSystems.Add(aiActionData.action);
            }

            ecsSystems.Add(new AiCleanUpPlanningDataSystem(actions));
        }
    }
}