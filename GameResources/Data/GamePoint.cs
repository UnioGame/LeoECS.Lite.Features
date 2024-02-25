namespace Game.Ecs.GameResources.Data
{
    using System;
    using Unity.Mathematics;
    using UnityEngine;

    [Serializable]
    public struct GamePoint
    {
        public static GamePoint Zero = new GamePoint()
        {
            Position = float3.zero,
            Rotation = Quaternion.identity,
            Scale = new float3(1,1,1),
        };

        public float3 Position;
        public quaternion Rotation;
        public float3 Scale;
    }
}