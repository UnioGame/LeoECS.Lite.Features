namespace Game.Ecs.Movement
{
    using Systems.NavMesh.Animation;
    using Cysharp.Threading.Tasks;
    using global::Animations.Animator.Data;
    using global::Movement.Systems.NavMesh.Animation;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Movement/Movement Animation Feature", 
        fileName = "Movement Animation Feature")]
    public sealed class MovementAnimationFeature : BaseLeoEcsFeature
    {
        public AnimationClipId idleClipId;
        public AnimationClipId walkClipId;
        public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new NavMeshMovementAnimationSystem());
            ecsSystems.Add(new MovementAnimatorSystem(idleClipId, walkClipId));
            return UniTask.CompletedTask;
        }
    }
}