namespace Game.Ecs.GameEffects.ImmobilityEffect
{
    using Systems;
    using Cysharp.Threading.Tasks;
    using Effects.Feature;
    using Leopotam.EcsLite;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Effects/Immobility Effect Feature")]
    public sealed class ImmobilityEffectFeature : EffectFeatureAsset
    {
        protected override UniTask OnInitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new ProcessImmobilityEffectSystem());
            ecsSystems.Add(new ProcessDestroyedBlockMovementEffectSystem());
            
            return UniTask.CompletedTask;
        }
    }
}