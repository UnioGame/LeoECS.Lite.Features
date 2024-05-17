﻿namespace Game.Ecs.AI.TargetOverride.Components
{
    using System;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct DistanceTargetOverrideComponent
    {
        public float DistanceSqr;
    }
}