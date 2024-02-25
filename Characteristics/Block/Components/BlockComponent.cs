namespace Game.Ecs.Characteristics.Block.Components
{
    using System;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif
    
    /// <summary>
    /// Значение параметра здоровья цели.
    /// </summary>
    [Serializable]
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public struct BlockComponent
    {
        /// <summary>
        /// Dodge value
        /// </summary>
        public float Value;
        
        /// <summary>
        /// Max dodge value
        /// </summary>
        public float MaxValue;
        
        /// <summary>
        /// min dodge value
        /// </summary>
        public float MinValue;
    }
}
