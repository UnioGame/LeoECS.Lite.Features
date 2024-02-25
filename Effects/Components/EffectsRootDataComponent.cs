namespace Game.Ecs.Effects.Components
{
    using System;
    using Data;

    /// <summary>
    /// config data for effects root
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct EffectsRootDataComponent
    {
        public EffectsRootData Value;
    }
}