namespace Game.Ecs.AbilityInventory.Components
{
    using System;
    using Leopotam.EcsLite;
    
    /// <summary>
    /// link to meta data for ability
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilityMetaLinkComponent
    {
        public EcsPackedEntity Value;
    }
}