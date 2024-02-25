namespace Game.Ecs.Ability.Common.Systems
{
    using Components;
    using Core.Components;
    using Core.Death.Components;
    using Leopotam.EcsLite;

    public sealed class SetAbilityNotActiveWhenDeadSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<PrepareToDeathComponent>()
                .Inc<AbilityMapComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var mapPool = _world.GetPool<AbilityMapComponent>();
            var activePool = _world.GetPool<ActiveAbilityComponent>();

            foreach (var entity in _filter)
            {
                ref var map = ref mapPool.Get(entity);
                foreach (var packedEntity in map.AbilityEntities)
                {
                    if(!packedEntity.Unpack(_world, out var abilityEntity))
                        continue;
                    
                    if(activePool.Has(abilityEntity))
                        activePool.Del(abilityEntity);
                }
            }
        }
    }
}