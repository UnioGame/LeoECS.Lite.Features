namespace MovementTweenAnimation
{
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Movement/Movement Tween Animation Feature")]
    public sealed class MovementTweenAnimationFeature : BaseLeoEcsFeature
    {
        public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new MovementTweenAnimatorSystem());
            
            return UniTask.CompletedTask;
        }
    }
}