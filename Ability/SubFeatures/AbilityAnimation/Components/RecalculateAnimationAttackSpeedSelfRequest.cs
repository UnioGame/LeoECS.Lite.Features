namespace Game.Ecs.Ability.SubFeatures.AbilityAnimation.Components
{
    using System;

    /// <summary>
    /// recalculate attack speed for self entity
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct RecalculateAnimationAttackSpeedSelfRequest
    {
    }
}