namespace Game.Ecs.Movement.Systems.NavMesh
{
    using System;
    using Aspect;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// disable navigation system
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class SetNavigationStatusByRequestSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;

        private NavigationAspect _navigationAspect;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<NavMeshAgentComponent>()
                .Inc<SetNavigationStatusSelfRequest>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var request = ref _navigationAspect.SetNavigationStatus.Get(entity);
                ref var navMeshAgentComponent = ref _navigationAspect.Agent.Get(entity);
                navMeshAgentComponent.Value.enabled = request.Value;
                
                _navigationAspect.SetNavigationStatus.Del(entity);
            }
        }
    }
}