namespace Game.Ecs.Characteristics.Base.Components.Requests
{
    using System;
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif

    /// <summary>
    /// recalculate characteristic value should be assign on characteristic entity
    /// </summary>
    [Serializable]
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public struct RecalculateCharacteristicSelfRequest
    {
        
    }
    
    /// <summary>
    /// recalculate modifications of characteristic by character target and characteristics type
    /// </summary>
    [Serializable]
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public struct RecalculateCharacteristicSelfRequest<TTarget>
    {
    }
}