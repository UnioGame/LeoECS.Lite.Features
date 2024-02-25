namespace Game.Ecs.Ability.SubFeatures.AbilitySequence.Components
{
    using System;

    /// <summary>
    /// mark ability sequence as ready to activate
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilitySequenceActiveComponent
    {
        public int Value;
    }
}