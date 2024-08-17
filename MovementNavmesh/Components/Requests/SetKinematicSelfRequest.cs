﻿namespace Game.Ecs.Movement.Components
{
    using System;

    /// <summary>
    /// disable navigation self
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct SetKinematicSelfRequest
    {
        public bool Value;
    }
}