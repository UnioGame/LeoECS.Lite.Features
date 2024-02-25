namespace Game.Ecs.Ability.SubFeatures.Selection.Components
{
    using System;
    using System.Runtime.CompilerServices;
    using Leopotam.EcsLite;
    using TargetSelection;
    using Unity.Mathematics;
    using UnityEngine.Serialization;
    /// <summary>
    /// Выбранные цели.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct SelectedTargetsComponent : IEcsAutoReset<SelectedTargetsComponent>
    {
        public static readonly EcsPackedEntity Empty = default;
        
        public EcsPackedEntity[] Entities;
        public int Count;
        
        public EcsPackedEntity[] PreviousEntities;
        public int PreviousCount;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetEntity(EcsPackedEntity entities)
        {
            CopyCurrent();
            Count = 1;
            Entities[0] = entities;
            MarkEmpty(Count);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetEmpty()
        {
            SetEntities(Array.Empty<EcsPackedEntity>(),0);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetEntities(EcsPackedEntity[] entities,int count)
        {
            CopyCurrent();

            Count = math.min(count, TargetSelectionData.MaxTargets);
            Count = math.max(0, Count);

            for (int i = 0; i < Count; i++)
                Entities[i] = entities[i];
            
            MarkEmpty(Count);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AutoReset(ref SelectedTargetsComponent c)
        {
            c.Entities ??= new EcsPackedEntity[TargetSelectionData.MaxTargets];
            c.Count = 0;
            
            c.PreviousEntities ??=  new EcsPackedEntity[TargetSelectionData.MaxTargets];
            c.PreviousCount = 0;
            
            c.MarkEmpty(0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CopyCurrent()
        {
            PreviousCount = Count;
            for (int i = 0; i < PreviousCount; i++)
                PreviousEntities[i] = Entities[i];
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void MarkEmpty(int start)
        {
            for (var i = start; i < TargetSelectionData.MaxTargets; i++)
                Entities[i] = Empty;
        }
    }
}