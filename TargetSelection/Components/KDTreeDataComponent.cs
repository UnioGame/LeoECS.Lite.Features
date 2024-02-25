namespace Game.Ecs.TargetSelection.Components
{
    using System;
    using Leopotam.EcsLite;
    using Unity.Mathematics;
    using UnityEngine;
    /// <summary>
    /// points cound for KD Tree
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct KDTreeDataComponent : IEcsAutoReset<KDTreeDataComponent>
    {
        public EcsPackedEntity[] PackedEntities;
        public float3[] Values;
        public int Count;
        
        public void AutoReset(ref KDTreeDataComponent c)
        {
            c.Values ??= new float3[TargetSelectionData.MaxAgents];
            c.PackedEntities ??= new EcsPackedEntity[TargetSelectionData.MaxAgents];
            c.Count = 0;
        }
    }
}