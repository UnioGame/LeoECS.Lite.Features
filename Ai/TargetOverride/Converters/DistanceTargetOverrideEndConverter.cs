namespace Game.Ecs.AI.TargetOverride.Converters
{
    using System;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.IL2CPP.CompilerServices;
    using UnityEngine;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
    public class DistanceTargetOverrideEndConverter : ITargetOverrideConverter
    {
        [SerializeField]
        private float _distance;
        
        public void Apply(EcsWorld world, int entity)
        {
            ref var distanceTargetOverrideEndComponent = ref world.AddComponent<DistanceTargetOverrideComponent>(entity);
            distanceTargetOverrideEndComponent.DistanceSqr = _distance * _distance;
        }
    }
}