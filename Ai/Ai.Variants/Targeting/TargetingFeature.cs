namespace Game.Ecs.GameAi.Targeting
{
    using AI.Components;
    using Characteristics.CriticalChance.Components;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using Systems;
    using UnityEngine;

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
    [CreateAssetMenu(menuName = "Game/Features/TargetingFeature", fileName = "Targeting Feature")]
    public class TargetingFeature : BaseLeoEcsFeature
    {
        public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new SelectByCategoryTargetSelectionSystem<AiSensorComponent>());
            ecsSystems.Add(new SelectByCategoryTargetSelectionSystem<AttackRangeComponent>());
            return UniTask.CompletedTask;
        }
    }
}