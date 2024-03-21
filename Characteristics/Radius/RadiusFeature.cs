namespace Game.Ecs.Characteristics.Radius
{
    using System;
    using Base;
    using Systems;
    using Component;
    using Cysharp.Threading.Tasks;
    using Feature;
    using Leopotam.EcsLite;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Characteristics/Radius Feature")]
    public sealed class RadiusFeature : CharacteristicFeature<RadiusEcsFeature>
    {
    }
    
    [Serializable]
    public sealed class RadiusEcsFeature : CharacteristicEcsFeature
    {
        protected override UniTask OnInitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.AddCharacteristic<RadiusComponent>();
            ecsSystems.Add(new RecalculateRadiusSystem());
            return UniTask.CompletedTask;
        }
    }
}