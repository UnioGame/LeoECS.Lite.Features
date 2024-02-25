namespace Game.Ecs.Characteristics.Cooldown.Systems
{
    using Base.Modification;
    using Components;
    using Leopotam.EcsLite;

    public sealed class RecalculateCooldownSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EcsPool<CooldownComponent> cooldownPool;
        private EcsPool<BaseCooldownComponent> baseCooldownPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<RecalculateCooldownSelfRequest>()
                .Inc<BaseCooldownComponent>()
                .Inc<CooldownComponent>()
                .End();
            
            baseCooldownPool = _world.GetPool<BaseCooldownComponent>();
            cooldownPool = _world.GetPool<CooldownComponent>();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var baseCooldown = ref baseCooldownPool.Get(entity);
                ref var cooldown = ref cooldownPool.Get(entity);

                cooldown.Value = baseCooldown.Modifications.Apply(baseCooldown.Value);
            }
        }
    }
}