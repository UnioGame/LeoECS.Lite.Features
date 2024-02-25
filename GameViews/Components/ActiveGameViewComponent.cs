namespace Game.Ecs.Gameplay.LevelProgress.Components
{
    using System;
    using Leopotam.EcsLite;

    /// <summary>
    /// current active view
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct ActiveGameViewComponent
    {
        public EcsPackedEntity Value;
    }

}