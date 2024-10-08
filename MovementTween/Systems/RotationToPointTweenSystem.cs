﻿namespace Game.Ecs.Movement.Systems.Transform
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using Unity.Mathematics;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
    
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class RotationToPointTweenSystem : IEcsRunSystem,IEcsInitSystem
    {
        private MovementTweenAspect _navigationAspect;
        private EcsFilter _filter;
        private EcsWorld _world;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<RotateToPointSelfRequest>()
                .Inc<TransformPositionComponent>()
                .Inc<TransformComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var transformComponent = ref _navigationAspect.Transform.Get(entity);
                ref var pointRequest = ref _navigationAspect.RotateTo.Get(entity);
                ref var positionComponent = ref _navigationAspect.Position.Get(entity);

                var transform = transformComponent.Value;
                
                if(transform == null) continue;
                
                var direction = math.normalize(pointRequest.Point - positionComponent.Position);
                var sign = math.sign(direction.x);
                var z = sign <= 0 ? 0 : 180;
                var rotation = transform.rotation;
                rotation.z = z;
                transform.rotation = rotation;
            }
        }
    }
}