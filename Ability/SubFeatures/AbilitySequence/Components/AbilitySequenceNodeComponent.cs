namespace Game.Ecs.Ability.SubFeatures.AbilitySequence.Components
{
    using System;
    using Leopotam.EcsLite;

    /// <summary>
    /// ability sequence node
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilitySequenceNodeComponent
    {
        public int Order;
        public int SequenceEntity;
    }
}