namespace Game.Ecs.Ability.SubFeatures.Target.Components
{
    using System.Runtime.CompilerServices;
    using Leopotam.EcsLite;
    using TargetSelection;
    using Unity.Mathematics;

    /// <summary>
    /// Компонент обозначающий, что сущность под целью другой сущности.
    /// </summary>
    public struct UnderTheTargetComponent : IEcsAutoReset<UnderTheTargetComponent>
    {
        public static readonly EcsPackedEntity Empty = default;
        
        public EcsPackedEntity[] Entities;
        public int Count;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetEntities(EcsPackedEntity[] entities,int count)
        {
            Count = math.min(count, TargetSelectionData.MaxTargets);

            for (int i = 0; i < Count; i++)
                Entities[i] = entities[i];

            MarkEmpty(Count);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void MarkEmpty(int start)
        {
            for (var i = start; i < TargetSelectionData.MaxTargets; i++)
                Entities[i] = Empty;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AutoReset(ref UnderTheTargetComponent c)
        {
            c.Entities ??= new EcsPackedEntity[TargetSelectionData.MaxTargets];
            c.Count = 0;
            c.MarkEmpty(0);
        }
    }
}