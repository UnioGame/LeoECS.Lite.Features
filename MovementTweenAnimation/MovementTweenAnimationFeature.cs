namespace Game.Ecs.Movement
{
    using Cysharp.Threading.Tasks;
    using global::Animations.Animator.Data;
    using global::Movement.Systems.NavMesh.Animation;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Movement/Movement Tween Animation Feature", 
        fileName = "Movement Tween Animation Feature")]
    public sealed class MovementTweenAnimationFeature : BaseLeoEcsFeature
    {
        public AnimationClipId idleClipId;
        public AnimationClipId walkClipId;
        
        public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new MovementTweenAnimatorSystem(idleClipId, walkClipId));
            return UniTask.CompletedTask;
        }
    }
}