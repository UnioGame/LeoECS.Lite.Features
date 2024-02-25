namespace Game.Ecs.Characteristics.Duration
{
    using Systems;
    using Components;
    using Cysharp.Threading.Tasks;
    using Feature;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.ExtendedSystems;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Characteristics/Duration Feature")]
    public sealed class DurationFeature : CharacteristicFeature
    {
        public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new RecalculateDurationSystem());
            ecsSystems.DelHere<RecalculateDurationRequest>();

            ecsSystems.Add(new ResetDurationSystem());
            
            return UniTask.CompletedTask;
        }
    }
}