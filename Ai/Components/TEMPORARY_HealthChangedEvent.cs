namespace Game.Ecs.AI.Components
{
    using System;
    using Leopotam.EcsLite;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    // TODO: Move to related modules later!!!
    public struct TEMPORARY_HealthChangedEvent
    {
        public EcsPackedEntity Dealer;
        public float Value;
    }
}