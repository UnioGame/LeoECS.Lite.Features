namespace Game.Ecs.Movement.Systems.Rigidbody
{
    using Components;
    using Core.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Components;
    using UnityEngine;

    /// <summary>
    /// Система отвечающая за поворот во время пермещения через физическую систему Unity.
    /// </summary>
    public sealed class RigidbodyRotationSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<RotationComponent>().Inc<RigidbodyComponent>().Inc<AngularSpeedComponent>().End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var quaternionPool = _world.GetPool<RotationComponent>();
            var transformPool = _world.GetPool<RigidbodyComponent>();
            var rotationSpeedPool = _world.GetPool<AngularSpeedComponent>();

            foreach (var entity in _filter)
            {
                ref var quaternionComponent = ref quaternionPool.Get(entity);
                ref var rigidbodyComponent = ref transformPool.Get(entity);
                ref var rotationSpeedComponent = ref rotationSpeedPool.Get(entity);

                var rigidbody = rigidbodyComponent.Value;
                
                var previous = rigidbody.rotation;
                var current = quaternionComponent.Value;

                var rotationSpeed = rotationSpeedComponent.Value;
                var angle = Quaternion.Angle(previous, current);
                var rotationTime = rotationSpeed / angle;

                var lerpRotation = Quaternion.Lerp(previous, current, rotationTime * Time.deltaTime);
                
                rigidbody.MoveRotation(lerpRotation);
            }
        }
    }
}