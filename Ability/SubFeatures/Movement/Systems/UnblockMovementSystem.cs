namespace Game.Ecs.Ability.SubFeatures.Movement.Systems
{
    using Common.Components;
    using Components;
    using Core.Components;
    using Ecs.Movement.Components;
    using Leopotam.EcsLite;
    using UnityEngine;

    public sealed class UnblockMovementSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<CanBlockMovementComponent>()
                .Inc<OwnerComponent>()
                .Inc<CompleteAbilitySelfRequest>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var ownerPool = _world.GetPool<OwnerComponent>();
            var blockPool = _world.GetPool<ImmobilityComponent>();

            foreach (var entity in _filter)
            {
                ref var owner = ref ownerPool.Get(entity);
                if(!owner.Value.Unpack(_world, out var ownerEntity))
                    continue;
                
                if(!blockPool.Has(ownerEntity))
                    continue;

                ref var block = ref blockPool.Get(ownerEntity);
                var blockCounter = block.BlockSourceCounter;
                blockCounter = Mathf.Clamp(blockCounter - 1, 0, int.MaxValue);
                block.BlockSourceCounter = blockCounter;
            }
        }
    }
}