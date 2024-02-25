namespace Game.Ecs.Gameplay.Death
{
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Gameplay/Death")]
    public class DeathFeature  : BaseLeoEcsFeature
    {
        public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new CheckReadyToDeathSystem());
            
            //if unit ready to death then create KillRequest
            ecsSystems.Add(new DetectReadyToDeathByHealthSystem());
            ecsSystems.Add(new ProcessDeathAnimationSystem());
            ecsSystems.Add(new EvaluateDeathAnimationSystem());
            ecsSystems.Add(new CompleteDeathAnimationSystem());
            
            return UniTask.CompletedTask;
        }
    }
}
