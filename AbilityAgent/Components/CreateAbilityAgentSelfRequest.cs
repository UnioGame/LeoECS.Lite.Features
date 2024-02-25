namespace Game.Ecs.AbilityAgent.Components
{
    using System;
    using Code.Configuration.Runtime.Ability;
    using Code.Configuration.Runtime.Ability.Description;
    using Leopotam.EcsLite;

    /// <summary>
    /// Self request to create ability agent.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct CreateAbilityAgentSelfRequest
    {
        public EcsPackedEntity Owner;
        public AbilityCell AbilityCell;
    }
}