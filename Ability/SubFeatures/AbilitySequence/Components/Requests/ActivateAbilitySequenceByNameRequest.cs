namespace Game.Ecs.Ability.SubFeatures.AbilitySequence
{
    using System;
    using Leopotam.EcsLite;

    /// <summary>
    /// activate ability sequence by name
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct ActivateAbilitySequenceByNameRequest
    {
        public string Name;
        public EcsPackedEntity Owner;
    }
}