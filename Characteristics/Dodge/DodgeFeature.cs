namespace Game.Ecs.Characteristics.Dodge
{
    using Base;
    using Components;
    using Cysharp.Threading.Tasks;
    using Feature;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.ExtendedSystems;
    using Systems;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Characteristics/Dodge Feature")]
    public sealed class DodgeFeature : CharacteristicFeature
    {
        public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.AddCharacteristic<DodgeComponent>();
            ecsSystems.Add(new RecalculateDodgeSystem());

            return UniTask.CompletedTask;
        }
    }
}