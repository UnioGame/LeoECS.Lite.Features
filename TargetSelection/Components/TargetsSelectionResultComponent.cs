using System.Collections.Generic;

namespace Game.Ecs.TargetSelection.Components
{
    using System;
    
    /// <summary>
    /// cache for range selection
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct TargetsSelectionResultComponent
    {
        public Dictionary<int, SqrRangeTargetSelectionResult> Results;
    }
}