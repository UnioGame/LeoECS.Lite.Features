namespace Game.Ecs.Ability.Common.Systems
{
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.IL2CPP.CompilerServices;

    /// <summary>
    /// Remove the ability pause component
    /// </summary>

#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public class RemovePauseAbilityRequestSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<RemovePauseAbilityRequest> _removePauseAbilityRequestPool;
        private EcsPool<AbilityPauseComponent> _pauseAbilityPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<RemovePauseAbilityRequest>()
                .End();
            _removePauseAbilityRequestPool = _world.GetPool<RemovePauseAbilityRequest>();
            _pauseAbilityPool = _world.GetPool<AbilityPauseComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var pauseAbilityRequest = ref _removePauseAbilityRequestPool.Get(entity);
                if (!pauseAbilityRequest.AbilityEntity.Unpack(_world, out var abilityEntity))
                    continue;
                _pauseAbilityPool.TryRemove(abilityEntity);
            }
        }
    }
}