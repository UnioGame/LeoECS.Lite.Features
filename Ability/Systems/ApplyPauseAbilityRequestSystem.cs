namespace Game.Ecs.Ability.Common.Systems
{
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.IL2CPP.CompilerServices;

    /// <summary>
    /// Add the ability suspension component to the ability
    /// </summary>

#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public class ApplyPauseAbilityRequestSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<PauseAbilityRequest> _pauseAbilityRequestPool;
        private EcsPool<AbilityPauseComponent> _pauseAbilityPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<PauseAbilityRequest>()
                .End();
            _pauseAbilityRequestPool = _world.GetPool<PauseAbilityRequest>();
            _pauseAbilityPool = _world.GetPool<AbilityPauseComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var pauseAbilityRequest = ref _pauseAbilityRequestPool.Get(entity);
                if (!pauseAbilityRequest.AbilityEntity.Unpack(_world, out var abilityEntity))
                    continue;
                _pauseAbilityPool.TryAdd(abilityEntity);
            }
        }
    }
}