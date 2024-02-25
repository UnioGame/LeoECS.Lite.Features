namespace Game.Ecs.GameEffects.DamageEffect
{
    using Systems;
    using Cysharp.Threading.Tasks;
    using Effects.Feature;
    using Leopotam.EcsLite;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Effects/Damage Effect Feature")]
    public sealed class DamageEffectFeature : EffectFeatureAsset
    {
        protected override UniTask OnInitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new ProcessDamageEffectSystem());
            ecsSystems.Add(new ProcessAttackDamageEffectSystem());
            ecsSystems.Add(new ProcessSplashAttackDamageEffectSystem());

            return UniTask.CompletedTask;
        }
    }
}