namespace Game.Ecs.Gameplay.Damage.Events
{
    using System;
    using Leopotam.EcsLite;

    /// <summary>
    /// notify about critical damage
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct CriticalDamageEvent
    {
        public float Value;
        public EcsPackedEntity Source;
        public EcsPackedEntity Destination;
    }
}