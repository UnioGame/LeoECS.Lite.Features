namespace Game.Ecs.Movement.Converters
{
    using System;
    using Unity.Mathematics;

    [Serializable]
    public struct MovementTrackPoint
    {
        public int track;
        public float3 position;
    }
}