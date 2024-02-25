namespace Game.Ecs.GameEffects.HealingEffect
{
    using Systems;
    using Cysharp.Threading.Tasks;
    using Effects.Feature;
    using Leopotam.EcsLite;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Effects/Healing Effect Feature")]
    public sealed class HealingEffectFeature : EffectFeatureAsset
    {
        protected override UniTask OnInitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new ProcessHealingEffectSystem());
            
            return UniTask.CompletedTask;
        }
    }
}