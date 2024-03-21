namespace Game.Ecs.Characteristics.Block
{
    using System;
    using Components;
    using Cysharp.Threading.Tasks;
    using Game.Ecs.Characteristics.Base;
    using Game.Ecs.Characteristics.Feature;
    using Leopotam.EcsLite;
    using Systems;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Characteristics/Block Feature")]
    public sealed class BlockFeature : CharacteristicFeature<BlockEcsFeature>
    {
    }
    
    [Serializable]
    public sealed class BlockEcsFeature : CharacteristicEcsFeature
    {
        protected override UniTask OnInitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.AddCharacteristic<BlockComponent>();
            ecsSystems.Add(new RecalculateBlockSystem());

            return UniTask.CompletedTask;
        }
    }
}