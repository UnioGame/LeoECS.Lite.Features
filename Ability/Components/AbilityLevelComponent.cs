namespace Game.Ecs.Ability.Components
{
    using System;

    /// <summary>
    /// Arena specific ability level component
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilityLevelComponent
    {
        public int Level;
    }
}