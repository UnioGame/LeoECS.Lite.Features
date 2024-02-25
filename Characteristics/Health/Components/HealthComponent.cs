namespace Game.Ecs.Characteristics.Health.Components
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
    public struct HealthComponent
    {
        /// <summary>
        /// Текущее значение здоровья.
        /// </summary>
        public float Health;
        
        /// <summary>
        /// Максимальное значение здовья.
        /// </summary>
        public float MaxHealth;
    }
}
