namespace Game.Ecs.Movement.Converters
{
    using System;
    using System.Collections.Generic;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.Collections;
    using Unity.Mathematics;
    using UnityEngine;

    [Serializable]
    public sealed class MovementTweenTrackConverter : LeoEcsConverter
    {
        public int trackId;
        
        public List<GameObject> movementData = new();
        
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            ref var trackComponent = ref world.AddComponent<MovementTweenTrackComponent>(entity);

            trackComponent.Id = trackId;
            trackComponent.Points = new NativeArray<MovementTrackPoint>(movementData.Count, Allocator.Persistent);

            for (var index = 0; index < movementData.Count; index++)
            {
                var point = movementData[index];
                var position = (float3)point.transform.position;
                
                trackComponent.Points[index] = new MovementTrackPoint
                {
                    position = position,
                    track = trackId,
                };
            }
        }
        
        
    }
}