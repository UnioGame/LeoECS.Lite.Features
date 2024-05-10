namespace Game.Ecs.Characteristics.CriticalChance.Components
{
    using System;
    using global::Characteristics.Radius.Abstract;
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif

    /// <summary>
    /// attack speed characteristic
    /// </summary>
    [Serializable]
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public struct AttackRangeComponent : IRadius
    {
        public float Radius => Value;
        
        public float Value;
    }
}