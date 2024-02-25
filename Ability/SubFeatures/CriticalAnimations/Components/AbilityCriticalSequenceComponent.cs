namespace Game.Ecs.Ability.SubFeatures.CriticalAnimations.Components
{
    using System;

    /// <summary>
    /// name of ability critical sequence
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilityCriticalSequenceComponent
    {
        public string Name;
    }
}