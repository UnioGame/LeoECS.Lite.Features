namespace Game.Ecs.GameAi.Move.Components
{
    using System;
    using AI.Data;
    using Leopotam.EcsLite;
    using Unity.Mathematics;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct MoveToTargetComponent : IEcsAutoReset<MoveToTargetComponent>
    {
        public float3 Value;
        public int Priority;
        public void AutoReset(ref MoveToTargetComponent c)
        {
            c.Value = default;
            c.Priority = AiConstants.PriorityNever;
        }
    }
}