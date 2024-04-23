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
    using Object = UnityEngine.Object;
    
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

            //add all planners
            for (var index = 0; index < actions.Length; index++)
            {
                var aiActionData = actions[index];
                var planner = aiActionData.planner;
                await planner.Initialize(index,ecsSystems);
            }

            //collect ai agents info
            //ecsSystems.Add(new AiCollectPlannerDataSystem(configuration));
            //make ai planning by ai agent data
            ecsSystems.Add(new AiPlanningSystem());
            //apply specific actions components
            ecsSystems.Add(new AiUpdatePlanningActionsStatusSystem(configuration.aiActions));

            //add all actions
            foreach (var aiActionData in configuration.aiActions)
                ecsSystems.Add(aiActionData.action);

            ecsSystems.Add(new AiCleanUpPlanningDataSystem(actions));
            
            //remove remove plan data
            ecsSystems.DelHere<AiAgentPlanningComponent>();
        }
    }
}