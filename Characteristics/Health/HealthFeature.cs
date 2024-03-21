namespace Game.Ecs.Characteristics.Health
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
    /// - recalculate health characteristic
    /// - check ready to death status if health <= 0
    /// - update helath value by request
    /// </summary>
    [CreateAssetMenu(menuName = "Game/Feature/Characteristics/Health Feature")]
    public sealed class HealthFeature : CharacteristicFeature<HealthEcsFeature>
    {
    }
    
    /// <summary>
    /// - recalculate health characteristic
    /// - check ready to death status if health <= 0
    /// - update helath value by request
    /// </summary>
    [Serializable]
    public sealed class HealthEcsFeature : CharacteristicEcsFeature
    {
        protected sealed override  UniTask OnInitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            //register health characteristic
            ecsSystems.AddCharacteristic<HealthComponent>();
            //update health by request
            ecsSystems.Add(new ProcessHealthChangedSystem());
            
            return UniTask.CompletedTask;
        }
    }
}