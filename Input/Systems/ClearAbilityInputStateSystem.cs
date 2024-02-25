namespace Game.Ecs.Input.Systems
{
    using Components.Ability;
    using Leopotam.EcsLite;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif

#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public sealed class ClearAbilityInputStateSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AbilityInputState>()
                .Exc<AbilityUpInputRequest>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var abilityInputStatePool = _world.GetPool<AbilityInputState>();

            foreach (var entity in _filter)
            {
                abilityInputStatePool.Del(entity);
            }
        }
    }
}