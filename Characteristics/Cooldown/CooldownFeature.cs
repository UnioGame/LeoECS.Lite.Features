namespace Game.Ecs.Characteristics.Cooldown
{
    using Systems;
    using Components;
    using Cysharp.Threading.Tasks;
    using Feature;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.ExtendedSystems;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Characteristics/Cooldown Feature")]
    public sealed class CooldownFeature : CharacteristicFeature
    {
        public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new RecalculateCooldownSystem());
            ecsSystems.DelHere<RecalculateCooldownSelfRequest>();
            ecsSystems.Add(new ResetCooldownSystem());
            
            return UniTask.CompletedTask;
        }
    }
}