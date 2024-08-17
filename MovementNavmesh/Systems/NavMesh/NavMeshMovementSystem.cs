namespace Game.Ecs.Movement.Systems.NavMesh
{
    using System;
    using Aspect;
    using Characteristics.Speed.Components;
    using Components;
    using Core.Components;
    using Core.Death.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// Система отвечающая за перемещение с помощью вектора скорости через систему NavMesh.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class NavMeshMovementSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private NavigationAspect _navigationAspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<VelocityComponent>()
                .Inc<NavMeshAgentComponent>()
                .Inc<SpeedComponent>()
                .Inc<AngularSpeedComponent>()
                .Inc<ChampionComponent>()
                .Inc<InstantRotateComponent>()
                .Exc<ImmobilityComponent>()
                .Exc<DisabledComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var velocityComponent = ref _navigationAspect.Velocity.Get(entity);
                ref var navMeshAgentComponent = ref _navigationAspect.Agent.Get(entity);
                ref var speedComponent = ref _navigationAspect.Speed.Get(entity);
                ref var rotationSpeedComponent = ref _navigationAspect.RotationSpeed.Get(entity);
                ref var instantRotateComponent = ref _navigationAspect.InstantRotate.Get(entity);

                var navMeshAgent = navMeshAgentComponent.Value;
                if (!navMeshAgent.enabled || !navMeshAgent.isOnNavMesh)
                    continue;

                if (instantRotateComponent.Value)
                {
                    var transform = navMeshAgent.transform;
                    var position = transform.position;
                    transform.LookAt(position + velocityComponent.Value);
                }

                navMeshAgent.speed = speedComponent.Value;
                navMeshAgent.angularSpeed = rotationSpeedComponent.Value;
                navMeshAgent.velocity = velocityComponent.Value * speedComponent.Value;
            }
        }
    }
}