namespace Game.Ecs.Gameplay.Death.Systems
{
    using System;
    using Core.Components;
    using Game.Ecs.Characteristics.Health.Components;
    using Game.Ecs.Core.Death.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    /// <summary>
    /// Add an empty target to an ability
    /// </summary>
    [Serializable]
    public class CheckReadyToDeathSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _readyFilter;
        private EcsPool<KillRequest> _killPool;
        private EcsPool<PrepareToDeathComponent> _readyPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _readyFilter = _world
                .Filter<PrepareToDeathComponent>()
                .Exc<AwaitDeathCompleteComponent>()
                .End();
            
            _killPool = _world.GetPool<KillRequest>();
            _readyPool = _world.GetPool<PrepareToDeathComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var readyEntity in _readyFilter)
            {
                ref var readyComponent = ref _readyPool.Get(readyEntity);
                ref var killRequest = ref _killPool.GetOrAddComponent(readyEntity);
                killRequest.Source = readyComponent.Source;
                
                _readyPool.Del(readyEntity);
            }
        }
    }
    
    
}