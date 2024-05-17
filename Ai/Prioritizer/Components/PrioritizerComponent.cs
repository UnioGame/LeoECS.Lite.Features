namespace Ai.Ai.Variants.Prioritizer.Components
{
    using System;
    using Data;
    using UnityEngine;

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct PrioritizerComponent
    {
        [SerializeReference]
        public IPriorityComparer[] Comparers;

        [SerializeReference]
        public IAgroCondition[] AgroConditions;
    }
}