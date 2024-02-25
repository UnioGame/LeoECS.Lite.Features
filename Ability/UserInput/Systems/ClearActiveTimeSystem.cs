namespace Game.Ecs.Ability.UserInput.Systems
{
    using Common.Components;
    using Input.Components;
    using Input.Components.Ability;
    using Leopotam.EcsLite;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif

#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public sealed class ClearActiveTimeSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AbilityMapComponent>()
                .Inc<UserInputTargetComponent>()
                .Exc<AbilityUpInputRequest>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var activateTimePool = _world.GetPool<AbilityActiveTimeComponent>();
            var abilityMapPool = _world.GetPool<AbilityMapComponent>();
            
            foreach (var entity in _filter)
            {
                ref var abilityMap = ref abilityMapPool.Get(entity);
                foreach (var packedEntity in abilityMap.AbilityEntities)
                {
                    if(!packedEntity.Unpack(_world, out var abilityEntity) || !activateTimePool.Has(abilityEntity))
                        continue;

                    ref var activateTime = ref activateTimePool.Get(abilityEntity);
                    activateTime.Time = 0.0f;
                }
            }
        }
    }
}