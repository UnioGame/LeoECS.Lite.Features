namespace Game.Ecs.Characteristics.Base.Components
{
    using System;
    using Leopotam.EcsLite;
    using UnityEngine.Serialization;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif
    
    /// <summary>
    /// link to characteristics entity
    /// </summary>
    [Serializable]
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public struct CharacteristicLinkComponent<TCharacteristic>
        where TCharacteristic : struct
    {
        [FormerlySerializedAs("Link")] public EcsPackedEntity Value;
    }
    
    /// <summary>
    /// link to characteristics entity
    /// </summary>
    [Serializable]
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public struct CharacteristicLinkComponent
    {
        public EcsPackedEntity Link;
    }
}