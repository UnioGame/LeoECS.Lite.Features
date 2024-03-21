namespace Game.Ecs.Characteristics.Attack
{
    using System;
    using Base;
    using Components;
    using Cysharp.Threading.Tasks;
    using Feature;
    using Leopotam.EcsLite;
    using Systems;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Characteristics/Attack Damage Feature")]
    public sealed class AttackDamageFeature : CharacteristicFeature<AttackDamageEcsFeature>
    {
    }
    
    [Serializable]
    public sealed class AttackDamageEcsFeature : CharacteristicEcsFeature
    {
        protected override UniTask OnInitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.AddCharacteristic<AttackDamageComponent>();
            ecsSystems.Add(new UpdateAttackDamageChangedSystem());

            return UniTask.CompletedTask;
        }
    }
}