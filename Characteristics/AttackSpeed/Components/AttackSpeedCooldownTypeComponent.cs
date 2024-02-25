namespace Game.Ecs.Characteristics.AttackSpeed.Components
{
    using System;
    using Ecs.Cooldown;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif

    /// <summary>
    /// attack speed cooldown type
    /// </summary>
    [Serializable]
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public struct AttackSpeedCooldownTypeComponent
    {
        public CooldownType Value;
    }
}