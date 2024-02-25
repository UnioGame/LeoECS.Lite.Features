namespace Game.Ecs.Characteristics.AbilityPower
{
    using Base;
    using Components;
    using Cysharp.Threading.Tasks;
    using Feature;
    using Leopotam.EcsLite;
    using Systems;
    using UnityEngine;
    
    /// <summary>
    /// provides a feature to increase the damage of abilities,
    /// allows you to change the strength of abilities by AbilityPowerComponent
    /// </summary>
    [CreateAssetMenu(menuName = "Game/Feature/Characteristics/AbilityPower Feature")]
    public sealed class AbilityPowerFeature : CharacteristicFeature
    {
        public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.AddCharacteristic<AbilityPowerComponent>();
            // update ability power value
            ecsSystems.Add(new RecalculateAbilityPowerSystem());

            return UniTask.CompletedTask;
        }
    }
}