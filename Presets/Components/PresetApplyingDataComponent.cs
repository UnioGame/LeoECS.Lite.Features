namespace Game.Ecs.Presets.Components
{
    using System;
    using Leopotam.EcsLite;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif
    
    /// <summary>
    /// data of applying process
    /// </summary>
    [Serializable]
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public struct PresetApplyingDataComponent
    {
        public EcsPackedEntity Source;
        public float Duration;
        public float StartTime;
    }
}