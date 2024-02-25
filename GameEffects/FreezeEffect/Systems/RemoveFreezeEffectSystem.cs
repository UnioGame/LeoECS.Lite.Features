namespace Game.Ecs.GameEffects.FreezeEffect.Systems
{
    using Components;
    using Leopotam.EcsLite;
    using Unity.IL2CPP.CompilerServices;

    /// <summary>
    /// Remove freeze effect
    /// </summary>

#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public class RemoveFreezeEffectSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<RemoveFreezeTargetEffectRequest> _removeFreezeTargetEffectPool;
        private EcsPool<FreezeTargetEffectComponent> _freezeTargetEffectPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<RemoveFreezeTargetEffectRequest>().End();
            _removeFreezeTargetEffectPool = _world.GetPool<RemoveFreezeTargetEffectRequest>();
            _freezeTargetEffectPool = _world.GetPool<FreezeTargetEffectComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var removeFreezeTargetEffectRequest = ref _removeFreezeTargetEffectPool.Get(entity);
                if (!removeFreezeTargetEffectRequest.Target.Unpack(_world, out var target))
                    continue;
                _freezeTargetEffectPool.Del(target);
            }
        }
    }
}