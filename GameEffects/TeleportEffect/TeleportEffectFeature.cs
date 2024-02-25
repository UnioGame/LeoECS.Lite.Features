namespace Game.Ecs.GameEffects.TeleportEffect
{
    using Systems;
    using Cysharp.Threading.Tasks;
    using Effects.Feature;
    using Leopotam.EcsLite;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Effects/Teleport Effect Feature")]
    public sealed class TeleportEffectFeature : EffectFeatureAsset
    {
        protected override UniTask OnInitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new ProcessTeleportEffectSystem());

            return UniTask.CompletedTask;
        }
    }
}