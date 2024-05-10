namespace NAMESPACE
{
    using Ai.Ai.Variants.Prioritizer.Systems;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using UniGame.Core.Runtime;
    using UniGame.Core.Runtime.Extension;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Bootstrap.Runtime.Config;
    using UnityEngine;

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
    [CreateAssetMenu(menuName = "Game/Features/PrioritizerFeature", fileName = "PrioritizerFeature")]
    public class PrioritizerFeature : BaseLeoEcsFeature
    {
        public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new CategoryPrioritizerSystem());
            return UniTask.CompletedTask;
        }
    }
}