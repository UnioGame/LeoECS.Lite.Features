namespace Game.Ecs.Characteristics.Dodge
{
    using System;
    using Base;
    using Components;
    using Cysharp.Threading.Tasks;
    using Feature;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.ExtendedSystems;
    using Systems;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Characteristics/Dodge Feature")]
    public sealed class DodgeFeature : CharacteristicFeature<DodgeEcsFeature>
    {
    }
    
    [Serializable]
    public sealed class DodgeEcsFeature : CharacteristicEcsFeature
    {
        protected override UniTask OnInitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.AddCharacteristic<DodgeComponent>();
            ecsSystems.Add(new RecalculateDodgeSystem());

            return UniTask.CompletedTask;
        }
    }
}