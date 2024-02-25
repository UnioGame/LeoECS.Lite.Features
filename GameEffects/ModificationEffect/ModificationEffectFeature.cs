namespace Game.Ecs.GameEffects.ModificationEffect
{
    using Systems;
    using Cysharp.Threading.Tasks;
    using Effects.Feature;
    using Leopotam.EcsLite;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Effects/Modification Effect Feature")]
    public sealed class ModificationEffectFeature : EffectFeatureAsset
    {
        protected override UniTask OnInitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new ProcessDestroyedModificationEffectSystem());
            ecsSystems.Add(new ProcessRemoveModificationEffectSystem());
            ecsSystems.Add(new ProcessSingleModificationEffectSystem());
            ecsSystems.Add(new ProcessModificationEffectSystem());

            return UniTask.CompletedTask;
        }
    }
}