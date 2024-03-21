namespace Game.Ecs.Characteristics.Shield
{
    using System;
    using Base;
    using Systems;
    using Components;
    using Cysharp.Threading.Tasks;
    using Feature;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.ExtendedSystems;
    using Speed.Components;
    using Speed.Systems;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Characteristics/Shield Feature")]
    public sealed class ShieldFeature : CharacteristicFeature<ShieldEcsFeature> {}
    
    [Serializable]
    public sealed class ShieldEcsFeature : CharacteristicEcsFeature
    {
        protected override UniTask OnInitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new ProcessShieldSystem());
            ecsSystems.DelHere<ChangeShieldRequest>();

            ecsSystems.Add(new ResetShieldSystem());
            
            return UniTask.CompletedTask;
        }
    }
}