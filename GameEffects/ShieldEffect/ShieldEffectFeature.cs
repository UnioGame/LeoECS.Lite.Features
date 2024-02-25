namespace Game.Ecs.GameEffects.ShieldEffect
{
    using Systems;
    using Cysharp.Threading.Tasks;
    using Effects.Feature;
    using Leopotam.EcsLite;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Effects/Shield Effect Feature")]
    public sealed class ShieldEffectFeature : EffectFeatureAsset
    {
        protected override UniTask OnInitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new ProcessShieldEffectSystem());
            ecsSystems.Add(new ProcessShieldValueEffectSystem());
            ecsSystems.Add(new ProcessDestroyedShieldEffectSystem());
            
            return UniTask.CompletedTask;
        }
    }
}