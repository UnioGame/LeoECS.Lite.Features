namespace Game.Ecs.AbilityInventory.Components
{
    using System;

    /// <summary>
    /// ability validation failed and should be destroyed
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilityValidationFailedComponent
    {
        
    }
}