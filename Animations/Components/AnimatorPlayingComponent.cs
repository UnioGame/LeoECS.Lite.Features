namespace Game.Ecs.Animations.Components
{
    using System;
    using Leopotam.EcsLite;

    /// <summary>
    /// current animation is playing
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AnimatorPlayingComponent
    {
        public EcsPackedEntity Value;
    }
}