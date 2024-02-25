namespace Game.Ecs.AbilityInventory.Components
{
    using System;
    using Unity.IL2CPP.CompilerServices;

    /// <summary>
    /// Search ability in inventory to champion
    /// </summary>
    [Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct EquipAbilityIdToChampionRequest
    {
        public int AbilityId;
        public int AbilitySlot;
        public bool IsUserInput;
        public bool IsDefault;
    }
}