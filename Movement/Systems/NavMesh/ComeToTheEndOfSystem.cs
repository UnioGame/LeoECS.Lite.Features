namespace Game.Ecs.Movement.Systems.NavMesh
{
    using System;
    using Aspect;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.Mathematics;
    using UnityEngine;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
    
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ComeToTheEndOfSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private NavigationAspect _navigationAspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<ComePointComponent>()
                .Inc<TransformComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var transform = ref _navigationAspect.Position.Get(entity);
                ref var comePoint = ref _navigationAspect.ComePoint.Get(entity);

                var currentPosition = transform.Position;
                var distance = math.distance(currentPosition, comePoint.Value);

                if (Mathf.Approximately(distance, 0.0f))
                {
                    _navigationAspect.ComePoint.Del(entity);
                    continue;
                }

                ref var movementPoint = ref _navigationAspect.MovementTargetPoint.GetOrAddComponent(entity);
                movementPoint.DestinationPosition = comePoint.Value;
            }
        }
    }
}