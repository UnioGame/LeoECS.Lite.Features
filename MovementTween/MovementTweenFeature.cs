namespace Game.Ecs.Movement
{
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Systems.Converters;
    using Systems.Transform;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UnityEngine;
    
    [CreateAssetMenu(menuName = "Game/Feature/Movement/Tween Movement Feature", fileName = "Tween Movement Feature")]
    public sealed class MovementTweenFeature : BaseLeoEcsFeature
    {
        public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new UpdateMovementTweenSystem());
            ecsSystems.Add(new ActivateMovementTweenSystem());
            ecsSystems.Add(new RotationToPointTweenSystem());
            ecsSystems.Add(new CheckTargetMovementTweenSystem());
            return UniTask.CompletedTask;
        }
    }
}