namespace Game.Ecs.GameEffects.ImmobilityEffect.Systems
{
    using Components;
    using Effects.Components;
    using Leopotam.EcsLite;
    using Movement.Components;

    public sealed class ProcessDestroyedBlockMovementEffectSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<ImmobilityEffectComponent>()
                .Inc<EffectComponent>()
                .Inc<DestroyEffectSelfRequest>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var effectPool = _world.GetPool<EffectComponent>();
            var blockMovementPool = _world.GetPool<ImmobilityComponent>();
            
            foreach (var entity in _filter)
            {
                ref var effect = ref effectPool.Get(entity);
                if(!effect.Destination.Unpack(_world, out var destinationEntity))
                    continue;

                if (!blockMovementPool.Has(destinationEntity))
                    continue;

                ref var block = ref blockMovementPool.Get(destinationEntity);
                block.BlockSourceCounter--;
            }
        }
    }
}