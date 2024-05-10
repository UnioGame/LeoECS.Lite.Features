namespace Game.Ecs.TargetSelection
{
    using System;
    using Leopotam.EcsLite;
    
    [Serializable]
    public struct SqrRangeTargetSelectionResult
    {
        public EcsPackedEntity[] Values;
        public EcsPackedEntity PrioritizedValue;
        public int Count;
        public bool Ready;
    }
}