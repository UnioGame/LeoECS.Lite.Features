namespace Game.Ecs.AI.Configurations
{
    using Sirenix.OdinInspector;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Configurations/AI/Ai Configuration",fileName = nameof(AiConfigurationAsset))]
    public class AiConfigurationAsset : ScriptableObject
    {
        [SerializeField]
        [InlineProperty]
        [HideLabel]
        public AiConfiguration configuration = new AiConfiguration();
    }
}
