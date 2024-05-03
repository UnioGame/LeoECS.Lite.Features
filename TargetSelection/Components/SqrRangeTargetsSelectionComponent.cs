using Leopotam.EcsLite;
using UniGame.LeoEcs.Shared.Abstract;
using UnityEngine.Networking;

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
    public struct SqrRangeTargetsSelectionComponent : 
        IApplyableComponent<SqrRangeTargetsSelectionComponent>
    {
        public SqrRangeTargetSelectionRequest[] Requests;
        public SqrRangeTargetSelectionResult[] Results;
        
        public void Apply(ref SqrRangeTargetsSelectionComponent component)
        {
            component.Requests = Requests;
            component.Results = Results;
        }
    }
}