namespace Game.Ecs.Gameplay.LevelProgress.Components
{
    using System;
    using UnityEngine;

    /// <summary>
    /// parent transform for all views
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct ViewRootComponent
    {
        public Transform Value;
    }
}