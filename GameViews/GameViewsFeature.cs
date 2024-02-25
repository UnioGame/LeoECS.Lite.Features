namespace Game.Ecs.Gameplay.LevelProgress
{
    using Components;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.ExtendedSystems;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/GameViews Feature",fileName = "GameViews Feature")]
    public class GameViewsFeature : BaseLeoEcsFeature
    {
        public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            //disable active view on activate new one
            ecsSystems.Add(new DisableGameViewOnActivateSystem());
            //disable active view
            ecsSystems.Add(new DisableActiveGameViewSystem());
            ecsSystems.Add(new RemoveActiveViewSystem());
            
            //make request to load new game view by resource id
            ecsSystems.Add(new ActivateGameViewSystem());

            //delete processed requests
            ecsSystems.DelHere<ActivateGameViewRequest>();
            ecsSystems.DelHere<DisableActiveGameViewRequest>();
            
            return UniTask.CompletedTask;
        }
    }
}
