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
    public class DistanceTargetOverrideLockConverter : ITargetOverrideConverter
    {
        [SerializeField]
        private float _lockDistance;

        [SerializeField]
        private float _releaseDistance;
        
        public void Apply(EcsWorld world, int entity)
        {
            ref var distanceTargetOverrideLockComponent = ref world.AddComponent<DistanceTargetOverrideLockComponent>(entity);
            distanceTargetOverrideLockComponent.LockDistanceSqr = _lockDistance * _lockDistance;
            distanceTargetOverrideLockComponent.ReleaseDistanceSqr = _releaseDistance * _releaseDistance;
        }
    }
}