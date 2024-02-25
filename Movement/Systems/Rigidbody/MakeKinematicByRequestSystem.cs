namespace Game.Ecs.Movement.Systems.NavMesh
{
    using System;
    using Aspect;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;

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
    public class MakeKinematicByRequestSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _rigidbodyFilter;

        private NavigationAspect _navigationAspect;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _rigidbodyFilter = _world
                .Filter<RigidbodyComponent>()
                .Inc<SetKinematicSelfRequest>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _rigidbodyFilter)
            {
                ref var request = ref _navigationAspect.SetKinematicStatus.Get(entity);
                ref var navMeshAgentComponent = ref _navigationAspect.Rigidbody.Get(entity);
                navMeshAgentComponent.Value.isKinematic = request.Value;
                _navigationAspect.SetKinematicStatus.Del(entity);
            }
        }
    }
}