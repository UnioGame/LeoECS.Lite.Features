namespace Game.Ecs.Characteristics.Shield.Systems
{
    using Components;
    using Leopotam.EcsLite;
    using UnityEngine;

    public sealed class ProcessShieldSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<ChangeShieldRequest>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var requestPool = _world.GetPool<ChangeShieldRequest>();
            var shieldPool = _world.GetPool<ShieldComponent>();

            foreach (var entity in _filter)
            {
                ref var request = ref requestPool.Get(entity);
                if(!request.Destination.Unpack(_world, out var destinationEntity) || !shieldPool.Has(destinationEntity))
                    continue;
                
                ref var shield = ref shieldPool.Get(destinationEntity);
                shield.Value += request.Value;
                
                if (shield.Value <= 0.0f || Mathf.Approximately(shield.Value, 0.0f))
                    shieldPool.Del(destinationEntity);
            }
        }
    }
}