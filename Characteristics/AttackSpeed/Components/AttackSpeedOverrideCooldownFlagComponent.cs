namespace Game.Ecs.Characteristics.AttackSpeed.Components
{
    using System;

    /// <summary>
    /// Обозначает, что мы переписали стандартное время перезарядки абилки
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AttackSpeedOverrideCooldownFlagComponent
    {
    }
}