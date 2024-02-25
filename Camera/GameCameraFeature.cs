namespace Game.Ecs.Camera
{
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Camera Feature", fileName = "Camera Feature")]
    public class GameCameraFeature : BaseLeoEcsFeature
    {
        public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new CameraLookAtTargetSystem());
            return UniTask.CompletedTask;
        }
    }
}