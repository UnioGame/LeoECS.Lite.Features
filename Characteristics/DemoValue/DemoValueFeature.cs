namespace Game.Ecs.Characteristics.DemoValue
{
    using System;
    using Components;
    using Cysharp.Threading.Tasks;
    using Game.Ecs.Characteristics.Base;
    using Game.Ecs.Characteristics.Feature;
    using Leopotam.EcsLite;
    using UnityEngine;

    /// <summary>
    /// - recalculate health characteristic
    /// - check ready to death status if health <= 0
    /// - update helath value by request
    /// </summary>
    [CreateAssetMenu(menuName = "Game/Feature/Characteristics/Demo Value Feature")]
    public sealed class DemoValueFeature : CharacteristicFeature<DemoValueEcsFeature>
    {
    }
    
    [Serializable]
    public sealed class DemoValueEcsFeature : CharacteristicEcsFeature
    {
        protected override UniTask OnInitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.AddCharacteristic<DemoValueComponent>();
            
            return UniTask.CompletedTask;
        }
    }
}