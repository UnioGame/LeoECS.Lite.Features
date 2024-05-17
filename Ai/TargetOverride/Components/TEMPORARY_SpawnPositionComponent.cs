namespace Game.Ecs.AI.TargetOverride.Components
{
    using System;
    using Unity.Mathematics;

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct TEMPORARY_SpawnPositionComponent
    {
        public float3 Value;
    }
}