namespace Game.Ecs.Characteristics.AttackSpeed
{
    using System;
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
    [CreateAssetMenu(menuName = "Game/Feature/Characteristics/AttackSpeed Feature")]
    public sealed class AttackSpeedFeature : CharacteristicFeature<AttackSpeedEcsFeature>
    {
    }
    
    [Serializable]
    public sealed class AttackSpeedEcsFeature : CharacteristicEcsFeature
    {
        protected override UniTask OnInitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            //update attack speed value
            ecsSystems.Add(new OverrideBaseAbilityCooldownSystem());//todo remove
            ecsSystems.Add(new UpdateAttackSpeedChangedSystem());
            ecsSystems.AddCharacteristic<AttackSpeedComponent>();
            
            return UniTask.CompletedTask;
        }
    }
}