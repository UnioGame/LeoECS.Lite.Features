namespace Game.Ecs.GameEffects.JumpForwardEffect
{
    using Component;
    using Cysharp.Threading.Tasks;
    using Effects.Feature;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.ExtendedSystems;
    using Systems;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Effects/Jump Forward Effect Feature")]
    public sealed class JumpForwardEffectFeature : EffectFeatureAsset
    {
        protected override UniTask OnInitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new CreateJumpForwardEffectSystem());
            ecsSystems.Add(new ApplyJumpForwardEffectSystem());
            ecsSystems.DelHere<JumpForwardEffectRequest>();
            ecsSystems.Add(new ProcessJumpForwardEffectSystem());

            return UniTask.CompletedTask;
        }
    }
}