namespace Game.Ecs.Characteristics.Base.Components.Requests
{
    using System;
    using Leopotam.EcsLite;
    using Modification;
    using Unity.IL2CPP.CompilerServices;

    /// <summary>
    /// Add modification of parameter
    /// </summary>
    [Serializable]
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public struct CreateModificationRequest
    {
        //modification source
        public EcsPackedEntity ModificationSource;
        //target characteristic
        public EcsPackedEntity Target;
        //modification value
        public Modification Modification;
    }
}