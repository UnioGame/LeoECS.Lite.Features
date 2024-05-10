using Game.Ecs.TargetSelection.Components;
using Leopotam.EcsLite.ExtendedSystems;

namespace Game.Ecs.TargetSelection
{
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Selection;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Target/Target Selection", fileName = "Target Selection Feature")]
    public class TargetSelectionFeature : BaseLeoEcsFeature
    {
        public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            var world = ecsSystems.GetWorld();
            
            var targetSelectionSystem = new TargetSelectionSystem();
            world.SetGlobal(targetSelectionSystem);
            
            ecsSystems.Add(targetSelectionSystem);
            //collect all valida target into targets component
            ecsSystems.Add(new InitKDTreeTargetsSystem());
            ecsSystems.Add(new CollectKDTreeTargetsSystem());
            ecsSystems.Add(new SelectAreaTargetsSystem());
            ecsSystems.DelHere<SqrRangeTargetsSelectionRequestComponent>();
            return UniTask.CompletedTask;
        }
    }
}