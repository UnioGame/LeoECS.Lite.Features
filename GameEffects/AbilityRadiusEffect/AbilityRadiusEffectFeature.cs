namespace Game.Ecs.GameEffects.DamageEffect
{
    using Cysharp.Threading.Tasks;
    using Effects.Feature;
    using Leopotam.EcsLite;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Effects/Ability Radius Effect Feature")]
    public sealed class AbilityRadiusEffectFeature : EffectFeatureAsset
    {
        protected override UniTask OnInitializeFeatureAsync(IEcsSystems ecsSystems)
        {

            return UniTask.CompletedTask;
        }
    }
}