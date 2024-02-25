namespace Game.Ecs.Ability.Components
{
    using System;
    using Code.Services.AbilityLoadout.Data;
    using UnityEngine.Serialization;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif

    /// <summary>
    /// Ability slot component type
    /// </summary>
    [Serializable]
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public struct AbilitySlotComponent
    {
        public int SlotType;
    }
}