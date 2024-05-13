namespace Ability.Components
{
    using System;

    /// <summary>
    /// Компонент, который хранит в себе базовое значение и текущее значение кулдауна способности
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilityCooldownComponent
    {
        public float baseCooldown;
        public float currentCooldown;
    }
}