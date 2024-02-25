namespace Game.Ecs.Movement.Systems.NavMesh
{
    using System;
    using Aspect;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// Система отвечающая за остановку симуляции NavMesh при наличии запроса на остановку <see cref="MovementStopRequest"/>.
    /// </summary>
    #if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class NavMeshAgentStopSimulationSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private NavigationAspect _navigationAspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<NavMeshAgentComponent>()
                .Inc<MovementStopRequest>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var navMeshAgent = ref _navigationAspect.Agent.Get(entity);

                var agent = navMeshAgent.Value;
                
                if(!navMeshAgent.Value.enabled || !agent.isOnNavMesh)
                    continue;
                
                navMeshAgent.Value.isStopped = true;
            }
        }
    }
}