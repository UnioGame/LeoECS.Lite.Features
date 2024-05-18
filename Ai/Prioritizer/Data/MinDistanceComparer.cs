namespace Ai.Ai.Variants.Prioritizer.Data
{
    using System;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Components;
    using Unity.Mathematics;
    using UnityEngine;

    [Serializable]
    public class MinDistanceComparer : IPriorityComparer
    {
        private EcsPool<TransformPositionComponent> _positionPool;

        public int Compare(EcsWorld world, int source, int current, int potential)
        {
            var positionPool = world.GetPool<TransformPositionComponent>();
            ref var sourcePositionComponent = ref positionPool.Get(source);
            ref var currentPositionComponent = ref positionPool.Get(current);
            ref var potentialPositionComponent = ref positionPool.Get(potential);

            var sourceToCurrentDistanceSqr = math.distancesq(sourcePositionComponent.Position, currentPositionComponent.Position);
            var sourceToPotentialDistanceSqr = math.distancesq(sourcePositionComponent.Position, potentialPositionComponent.Position);

            if (Mathf.Approximately(sourceToCurrentDistanceSqr, sourceToPotentialDistanceSqr))
            {
                return 0;
            }

            if (sourceToPotentialDistanceSqr < sourceToCurrentDistanceSqr)
            {
                return 1;
            }
            
            return -1;
        }
    }
}