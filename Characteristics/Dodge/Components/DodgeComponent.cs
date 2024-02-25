namespace Game.Ecs.Characteristics.Dodge.Components
{
    using System;
    

    /// <summary>
    /// Значение параметра здоровья цели.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct DodgeComponent
    {
        /// <summary>
        /// Dodge value
        /// </summary>
        public float Value;
    }
}
