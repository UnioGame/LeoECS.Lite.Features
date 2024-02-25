namespace Game.Ecs.Characteristics.Base.Components.Events
{
    using System;
    using Leopotam.EcsLite;
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif

    /// <summary>
    /// value of characteristic changed
    /// </summary>
    [Serializable]
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public struct CharacteristicValueChangedEvent
    {
        public EcsPackedEntity Owner;
        public EcsPackedEntity Characteristic;
        public float PreviousValue;
        public float Value;
    }
    
    /// <summary>
    /// value of characteristic changed
    /// </summary>
    [Serializable]
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public struct CharacteristicValueChangedEvent<TCharacteristic>
        where TCharacteristic : struct
    {
        public EcsPackedEntity Owner;
        public EcsPackedEntity Characteristic;
        public float PreviousValue;
        public float Value;
    }
}