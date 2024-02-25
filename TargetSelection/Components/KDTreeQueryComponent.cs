namespace Game.Ecs.TargetSelection.Components
{
    using System;
    using DataStructures.ViliWonka.KDTree;
    /// <summary>
    /// radius query for KDTree
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct KDTreeQueryComponent
    {
        public KDQuery Value;
    }
}