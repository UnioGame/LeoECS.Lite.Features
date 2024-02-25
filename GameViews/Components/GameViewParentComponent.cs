namespace Game.Ecs.Gameplay.LevelProgress.Components
{
    using System;
    using UnityEngine;

    /// <summary>
    /// game view container parent
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct GameViewParentComponent
    {
        public Transform Parent;
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;
    }
}