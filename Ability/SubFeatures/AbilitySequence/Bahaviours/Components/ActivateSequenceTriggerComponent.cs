namespace Game.Ecs.Ability.SubFeatures.AbilitySequence.Bahaviours.Components
{
    using System;
    using Leopotam.EcsLite;

    /// <summary>
    /// trigger ability sequence to activate with ability start
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct ActivateSequenceTriggerComponent
    {
        public int Sequence;
        public int Trigger;
    }
}