namespace Game.Ecs.Movement.Components
{
    using System;

    /// <summary>
    /// Параметр мгновенного вращения.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct InstantRotateComponent
    {
        public bool Value;
    }
}