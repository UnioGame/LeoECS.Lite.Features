namespace Game.Ecs.GameAi.MoveToTarget.Components
{
    using System;
    using System.Collections.Generic;
    using Code.Configuration.Runtime.Effects.Abstract;
    using Unity.Mathematics;

    [Serializable]
    public struct MoveToTargetActionComponent
    {
        public float3 Position;
    }
}
