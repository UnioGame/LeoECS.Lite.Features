namespace Game.Ecs.Gameplay.CriticalAttackChance
{
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Gameplay/Critical Attack Chance Feature",fileName = "Critical Attack Chance Feature")]
    public class CriticalAttackChanceFeature  : BaseLeoEcsFeature
    {
        public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            
            //recalculate critical change after attack and mark with CriticalAttackMarkerComponent
            //if next attack should be critical
            ecsSystems.Add(new RecalculateCriticalChanceSystem());
            //on new circle recalculate critical chance after damage event ready to gone
            //detect attack and check can it be critical or not
            ecsSystems.Add(new DetectAttackDamageEventSystem());
            
            return UniTask.CompletedTask;
        }
    }
}
