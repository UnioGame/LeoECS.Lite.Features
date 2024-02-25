namespace Game.Ecs.Ability.Components
{
    using System;
    using Leopotam.EcsLite;

    /// <summary>
    /// link ability and it's animation variations
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilityAnimationComponent
    {
        public EcsPackedEntity Ability;
    }
}