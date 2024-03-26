namespace Game.Ecs.AbilityInventory.Components
{
    using System;
    using Leopotam.EcsLite;
    using UnityEngine.Serialization;
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif

    /// <summary>
    /// Notify about ability equip changed
    /// </summary>
    [Serializable]
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public struct AbilityEquipChangedEvent
    {
        public int AbilityId;
        public int AbilitySlot;
        public EcsPackedEntity Owner;
        public EcsPackedEntity AbilityEntity;
    }
}