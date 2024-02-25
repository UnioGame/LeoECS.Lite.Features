namespace Game.Ecs.Ability.Components.Requests
{
    using System;
    using UnityEngine.Serialization;

    /// <summary>
    /// set base value cooldown of default ability
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct SetAbilityBaseCooldownSelfRequest
    {
        public float Cooldown;
        public int AbilitySlot;
    }
}