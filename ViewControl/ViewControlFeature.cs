namespace Game.Ecs.ViewControl
{
    using Systems;
    using Components;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.ExtendedSystems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UnityEngine;
    
    [CreateAssetMenu(menuName = "Game/Feature/View Control Feature", fileName = "View Control Feature")]
    public sealed class ViewControlFeature : BaseLeoEcsFeature
    {
        public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new ProcessHideViewRequestSystem());
            ecsSystems.DelHere<HideViewRequest>();
            
            ecsSystems.Add(new ProcessShowViewRequestSystem());
            ecsSystems.DelHere<ShowViewRequest>();

            ecsSystems.Add(new ProcessDestroyedOwnerViewSystem());
            
            return UniTask.CompletedTask;
        }
    }
}