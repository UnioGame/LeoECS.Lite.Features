namespace Game.Ecs.Ai.Targeting.Converters
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
    public class OverDistanceTargetingLockConverter : ITargetSelectorConverter
    {
        [SerializeField]
        private float _lockDistance;

        [SerializeField]
        private float _releaseDistance;
        
        public void Apply(EcsWorld world, int entity)
        {
            ref var overDistanceLockComponent = ref world.AddComponent<OverDistanceTargetingLockComponent>(entity);
            overDistanceLockComponent.LockDistanceSqr = _lockDistance * _lockDistance;
            overDistanceLockComponent.ReleaseDistanceSqr = _releaseDistance * _releaseDistance;
        }
    }
}