namespace Ai.Ai.Variants.Prioritizer.Data
{
    using System;
    using Game.Ecs.AI.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Components;
    using Unity.Mathematics;

    [Serializable]
    public class AgroRangeCondition : IAgroCondition
    {
        public bool Check(EcsWorld world, int source, int target)
        {
            var agroPool = world.GetPool<AiAgroRangeComponent>();
            var transformPositionPool = world.GetPool<TransformPositionComponent>();
            ref var agroComponent = ref agroPool.Get(source);
            ref var sourcePositionComponent = ref transformPositionPool.Get(source);
            ref var targetPositionComponent = ref transformPositionPool.Get(target);

            return math.distancesq(sourcePositionComponent.Position, targetPositionComponent.Position) 
                   <= agroComponent.RangeSqr;
        }
    }
}