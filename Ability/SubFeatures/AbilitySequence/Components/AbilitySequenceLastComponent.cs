namespace Game.Ecs.Ability.SubFeatures.AbilitySequence.Components
{
    using System;

    /// <summary>
    /// link to last ability in sequence
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilitySequenceLastComponent
    {
        public int Value;
    }
}