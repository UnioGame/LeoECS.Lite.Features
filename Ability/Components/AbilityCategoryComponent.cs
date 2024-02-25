namespace Game.Ecs.Ability.Components
{
    using System;
    using Code.Services.Ability.Data.Arena;
    /// <summary>
    /// Arena specific Ability category component
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilityCategoryComponent
    {
        public AbilityCategoryId Id;
    }
}