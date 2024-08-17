namespace Game.Ecs.Movement.Systems.NavMesh
{
    using System;
    using Aspect;
    using Components;
    using Core.Death.Components;
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
    public sealed class DisableNavMeshAgentSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsWorld _world;
        
        private EcsFilter _filter;
        private EcsFilter _excFilter;
        
        private NavigationAspect _navigationAspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<NavMeshAgentComponent>()
                .Inc<DisabledComponent>()
                .End();
            
            _excFilter = _world
                .Filter<NavMeshAgentComponent>()
                .Exc<DisabledComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var navMeshAgentComponent = ref _navigationAspect.Agent.Get(entity);
                if(navMeshAgentComponent.Value.enabled)
                    navMeshAgentComponent.Value.enabled = false;
            }
            
            foreach (var entity in _excFilter)
            {
                ref var navMeshAgentComponent = ref _navigationAspect.Agent.Get(entity);
                if(!navMeshAgentComponent.Value.enabled)
                    navMeshAgentComponent.Value.enabled = true;
            }
        }
    }
}