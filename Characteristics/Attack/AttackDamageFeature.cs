namespace Game.Ecs.Characteristics.Attack
{
    using Base;
    using Components;
    using Cysharp.Threading.Tasks;
    using Feature;
    using Leopotam.EcsLite;
    using Systems;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Characteristics/Attack Damage Feature")]
    public sealed class AttackDamageFeature : CharacteristicFeature
    {
        public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.AddCharacteristic<AttackDamageComponent>();
            ecsSystems.Add(new UpdateAttackDamageChangedSystem());

            return UniTask.CompletedTask;
        }
    }
}