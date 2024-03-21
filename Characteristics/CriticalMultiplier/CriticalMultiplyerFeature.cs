namespace Game.Ecs.Characteristics.CriticalMultiplier
{
    using System;
    using Components;
    using Cysharp.Threading.Tasks;
    using Base;
    using Feature;
    using Leopotam.EcsLite;
    using Systems;
    using UnityEngine;

    /// <summary>
    /// - recalculate attack speed characteristic
    /// </summary>
    [CreateAssetMenu(menuName = "Game/Feature/Characteristics/Critical Multiplier Feature",fileName = "Critical Multiplier")]
    public sealed class CriticalMultiplierFeature : CharacteristicFeature<CriticalMultiplierEcsFeature>
    {
    }
    
    [Serializable]
    public sealed class CriticalMultiplierEcsFeature : CharacteristicEcsFeature
    {
        protected override UniTask OnInitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            //register health characteristic
            ecsSystems.AddCharacteristic<CriticalMultiplierComponent>();
            //update attack speed value
            ecsSystems.Add(new UpdateCriticalChanceChangedSystem());
            
            return UniTask.CompletedTask;
        }
    }
}