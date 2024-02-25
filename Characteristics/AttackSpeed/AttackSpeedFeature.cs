namespace Game.Ecs.Characteristics.AttackSpeed
{
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
    public sealed class AttackSpeedFeature : CharacteristicFeature
    {
        public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            //register health characteristic
            ecsSystems.AddCharacteristic<AttackSpeedComponent>();
            //update attack speed value
            ecsSystems.Add(new UpdateAttackSpeedChangedSystem());

            return UniTask.CompletedTask;
        }
    }
}