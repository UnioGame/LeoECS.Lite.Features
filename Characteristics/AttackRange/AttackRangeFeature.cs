namespace Game.Ecs.Characteristics.CriticalChance
{
    using System;
    using Attack.Components;
    using Components;
    using Cysharp.Threading.Tasks;
    using Game.Ecs.Characteristics.Base;
    using Game.Ecs.Characteristics.Feature;
    using Leopotam.EcsLite;
    using Systems;
    using UnityEngine;

    /// <summary>
    /// - recalculate attack speed characteristic
    /// </summary>
    [CreateAssetMenu(menuName = "Game/Feature/Characteristics/AttackRange Feature",fileName = "AttackRange Feature")]
    public sealed class CriticalChanceFeature : CharacteristicFeature<CriticalChanceEcsFeature>
    {
    }
    
    [Serializable]
    public sealed class CriticalChanceEcsFeature : CharacteristicEcsFeature
    {
        protected override UniTask OnInitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            //register health characteristic
            ecsSystems.AddCharacteristic<AttackRangeComponent>();
            //update attack speed value
            ecsSystems.Add(new UpdateAttackRangeChangedSystem());
            
            return UniTask.CompletedTask;
        }
    }
}