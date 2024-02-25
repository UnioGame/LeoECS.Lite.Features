namespace Game.Ecs.AbilityAgent.Components
{
    using System;
    using Game.Code.Configuration.Runtime.Ability;

    /// <summary>
    /// Say that this entity is an ability agent
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilityAgentComponent
    {
        public AbilityCell Value;
    }
}