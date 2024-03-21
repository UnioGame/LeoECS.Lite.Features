namespace Game.Ecs.Characteristics.Speed
{
    using System;
    using Base;
    using Systems;
    using Components;
    using Cysharp.Threading.Tasks;
    using Feature;
    using Leopotam.EcsLite;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Characteristics/Speed Feature")]
    public class SpeedFeature : CharacteristicFeature<SpeedEcsFeature>
    {
    }
    
    [Serializable]
    public sealed class SpeedEcsFeature : CharacteristicEcsFeature
    {
        protected override UniTask OnInitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.AddCharacteristic<SpeedComponent>();
            ecsSystems.Add(new RecalculateSpeedSystem());
            
            return UniTask.CompletedTask;
        }
    }
}