namespace Game.Ecs.Animations
{
    using Components.Requests;
    using Cysharp.Threading.Tasks;
    using Data;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.ExtendedSystems;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    /// <summary>
    /// Spawner game feature
    /// </summary>
    [CreateAssetMenu(menuName = "Game/Feature/Animations Feature", fileName = "Animations Feature")]
    public class AnimationsFeature : BaseLeoEcsFeature
    {
        public float minimumPlayableSpeed = 0.1f;
        
        public sealed override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            var world = ecsSystems.GetWorld();
            var animationTool = new AnimationToolSystem();
            world.SetGlobal(animationTool);
            
            ecsSystems.Add(animationTool);
                        
            //if animation has link data when apply it before play
            ecsSystems.Add(new CreateAnimationLinkDataSystem());
            //prepare timeline animation data
            ecsSystems.Add(new CreateTimeLineAnimationSystem());
            
            //play timeline animation
            ecsSystems.Add(new PlayTimelineAnimationSystem());
            
            //update playing animation status
            ecsSystems.Add(new EvaluateAnimationSystem());
            
            ecsSystems.Add(new HandlePlayOnPlayableSystem());
            //if get request to play animation when it playing
            ecsSystems.Add(new HandlePlayRequestAtPlayingDirectorSystem());
            
            //stop playable director and animation
            ecsSystems.Add(new CompletePlayableAnimatorSystem());
            //clean timeline animation data of stop request
            ecsSystems.Add(new CompletePlayableAnimationSystem());
            //kill animation entity if kill on complete component exists
            ecsSystems.Add(new KillCompletedAnimationSystem());
            
            ecsSystems.Add(new PlayOnPlayableDirectorSystem(minimumPlayableSpeed));
            
            //remove stop animation
            ecsSystems.DelHere<PlayOnDirectorSelfRequest>();
            ecsSystems.DelHere<PlayAnimationSelfRequest>();
            
            ecsSystems.DelHere<PreparePlayableAnimatorSelfRequest>();
            ecsSystems.DelHere<StopAnimationSelfRequest>();
            
            ecsSystems.DelHere<CreateAnimationLinkSelfRequest>();
            ecsSystems.DelHere<CreateAnimationPlayableSelfRequest>();
            
            return UniTask.CompletedTask;
        }
    }
}