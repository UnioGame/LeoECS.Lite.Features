namespace Game.Ecs.Characteristics.Duration
{
    using System;
    using Base.Modification;
    using Components;
    using Leopotam.EcsLite;

    [Serializable]
    public sealed class DurationModificationHandler : ModificationHandler
    {
        public override void AddModification(EcsWorld world,int source, int destinationEntity)
        {
            var baseDurationPool = world.GetPool<BaseDurationComponent>();
            if(!baseDurationPool.Has(destinationEntity))
                return;

            ref var baseDuration = ref baseDurationPool.Get(destinationEntity);
            baseDuration.Modifications.AddModification(Modification);
            
            var requestPool = world.GetPool<RecalculateDurationRequest>();
            if (!requestPool.Has(destinationEntity))
                requestPool.Add(destinationEntity);
        }

        public override void RemoveModification(EcsWorld world,int source, int destinationEntity)
        {
            var baseDurationPool = world.GetPool<BaseDurationComponent>();
            if(!baseDurationPool.Has(destinationEntity))
                return;

            ref var baseDuration = ref baseDurationPool.Get(destinationEntity);
            baseDuration.Modifications.RemoveModification(Modification);
            
            var requestPool = world.GetPool<RecalculateDurationRequest>();
            if (!requestPool.Has(destinationEntity))
                requestPool.Add(destinationEntity);
        }
    }
}