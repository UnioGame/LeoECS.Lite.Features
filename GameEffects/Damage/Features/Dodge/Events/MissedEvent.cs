namespace Game.Ecs.Gameplay.Dodge.Events
{
    using System;
    using Leopotam.EcsLite;
    using Unity.IL2CPP.CompilerServices;

    /// <summary>
    /// attack miss event
    /// </summary>
    [Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct MissedEvent
    {
        public EcsPackedEntity Source;
        public EcsPackedEntity Destination;
    }
}