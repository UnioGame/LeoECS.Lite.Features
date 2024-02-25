namespace Game.Ecs.Characteristics.SplashDamage
{
    using Base;
    using Components;
    using Cysharp.Threading.Tasks;
    using Feature;
    using Leopotam.EcsLite;
    using Systems;
    using UnityEngine;
    
    /// <summary>
    /// allows you to deal damage on the area with default attacks, 
    /// the characteristic increases the damage that opponents receive near the main target
    /// </summary>
    [CreateAssetMenu(menuName = "Game/Feature/Characteristics/SplashDamage Feature")]
    public sealed class SplashDamageFeature : CharacteristicFeature
    {
        public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.AddCharacteristic<SplashDamageComponent>();
            // update Splash Damage value
            ecsSystems.Add(new RecalculateSplashDamageSystem());

            return UniTask.CompletedTask;
        }
    }
}