namespace Game.Ecs.Movement.Systems
{
    using System;
    using Aspect;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessImmobilityStatusSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsFilter _stopFilter;
        
        private EcsWorld _world;

        private NavigationAspect _navigationAspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<ImmobilityComponent>().End();
            _stopFilter = _world.Filter<ImmobilityComponent>()
                .Inc<NavMeshAgentComponent>()
                .Exc<MovementStopRequest>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var block = ref _navigationAspect.Immobility.Get(entity);
                if (block.BlockSourceCounter <= 0)
                    _navigationAspect.Immobility.Del(entity);
            }
            
            foreach (var entity in _stopFilter)
            {
                if(_navigationAspect.NavMeshAgentStop.Has(entity))
                    continue;

                _navigationAspect.NavMeshAgentStop.Add(entity);
            }
        }
    }
}