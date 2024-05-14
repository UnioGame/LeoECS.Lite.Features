namespace Game.Ecs.AI.Configurations
{
    using global::Ai.Ai.Variants.Prioritizer.Data;
    using Sirenix.OdinInspector;
    using Targeting.Data;
    using UniGame.LeoEcs.Converter.Runtime.Abstract;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Configurations/AI/Ai Agent Configuration",fileName = nameof(AiConfigurationAsset))]
    public class AiAgentConfigurationAsset : ScriptableObject, ILeoEcsGizmosDrawer
    {
        [SerializeField]
        [InlineProperty] 
        [HideLabel]
        public TargetingConfig targetingConfig;

        [SerializeField]
        [HideLabel]
        public PrioritizerConfig prioritizerConfig;
        
        [SerializeField]
        public AiCommonPlanners[] commonAiConverters;
        
        [SerializeField]
        [InlineProperty] 
        [HideLabel] 
        public AiAgentConfiguration agentConfiguration = new AiAgentConfiguration();
        
        [SerializeField]
        [InlineEditor]
        public AiConfigurationAsset aiConfiguration;

        public int ActionsCount => aiConfiguration
            .configuration
            .aiActions.Length;
        
        public void DrawGizmos(GameObject target)
        {
            foreach (var planner in agentConfiguration.Planners)
            {
                if(planner is not ILeoEcsGizmosDrawer drawer) continue;
                drawer.DrawGizmos(target);
            }
        }
    }
}