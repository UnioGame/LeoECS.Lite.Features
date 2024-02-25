namespace Game.Ecs.Ability.Components.Requests
{
    using System;
    using Leopotam.EcsLite;

    /// <summary>
    /// request to take ability in hand and activate it
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct ActivateAbilityRequest
    {
        public EcsPackedEntity Ability;
        public EcsPackedEntity Target;
    }
}