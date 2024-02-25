namespace Game.Ecs.Characteristics.Base.Components
{
    using System;
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif

    /// <summary>
    /// default characteristic value at start == basevalue
    /// </summary>
    [Serializable]
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public struct CharacteristicDefaultValueComponent
    {
        public float BaseValue;
        public float Value;
        public float MaxValue;
        public float MinValue;
    }
}