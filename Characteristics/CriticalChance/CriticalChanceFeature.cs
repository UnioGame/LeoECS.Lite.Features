namespace Game.Ecs.Characteristics.CriticalChance
{
    using System;
    using Base;
    using Components;
    using Cysharp.Threading.Tasks;
    using Feature;
    using Leopotam.EcsLite;
    using Systems;
    using UnityEngine;

    /// <summary>
    /// - recalculate attack speed characteristic
    /// </summary>
    [CreateAssetMenu(menuName = "Game/Feature/Characteristics/Critical Chance Feature",fileName = "Critical Chance")]
    public sealed class CriticalChanceFeature : CharacteristicFeature<CriticalChanceEcsFeature>
    {
    }
    
    [Serializable]
    public sealed class CriticalChanceEcsFeature : CharacteristicEcsFeature
    {
        protected override UniTask OnInitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            //register health characteristic
            ecsSystems.AddCharacteristic<CriticalChanceComponent>();
            //update attack speed value
            ecsSystems.Add(new UpdateCriticalChanceChangedSystem());
            
            return UniTask.CompletedTask;
        }
    }
}