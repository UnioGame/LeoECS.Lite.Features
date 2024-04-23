namespace Game.Ecs.AI.Configurations
{
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime.Abstract;
    using UnityEngine;
    using UnityEngine.Serialization;

    [CreateAssetMenu(menuName = "Game/Configurations/AI/Ai Agent Configuration",fileName = nameof(AiConfigurationAsset))]
    public class AiAgentConfigurationAsset : ScriptableObject,ILeoEcsGizmosDrawer
    {
        [FormerlySerializedAs("_agentConfiguration")]
        [SerializeField]
        [InlineProperty] 
        [HideLabel] 
        public AiAgentConfiguration agentConfiguration = new AiAgentConfiguration();

        [FormerlySerializedAs("_aiConfiguration")]
        [SerializeField]
        [InlineEditor()]
        public AiConfigurationAsset aiConfiguration;
        
        public AiAgentConfiguration AiAgentConfiguration => agentConfiguration;

        public AiConfiguration AiConfiguration => aiConfiguration.configuration;

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