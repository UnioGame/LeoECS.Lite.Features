namespace Game.Ecs.GameAi.MoveToTarget.Systems
{
    using System;
    using Ability.Tools;
    using AI.Abstract;
    using Components;
    using Core.Death.Components;
    using Effects;
    using Game.Ecs.AI.Components;
    using Game.Ecs.Movement.Components;
    using Gameplay.LevelProgress.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.Mathematics;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class MoveToTargetAiSystem : IAiActionSystem,IEcsInitSystem
    {
        public float minSqrDistance = 4f;
        
        private EcsFilter _filter;
        private EcsWorld _world;
        private AbilityTools _abilityTools;
        
        private EcsPool<TransformPositionComponent> _transformPool;
        private EcsPool<TransformDirectionComponent> _directionPool;
        private EcsPool<MoveToTargetActionComponent> _moveToTargetPool;
        private EcsPool<MovementPointRequest> _movementPointPool;
        private EcsPool<RotateToPointSelfRequest> _rotateToPointPool;
        private EcsPool<ActiveGameViewComponent> _viewPool;
        private EcsPool<NavMeshAgentComponent> _agentPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _abilityTools = _world.GetGlobal<AbilityTools>();
            
            _filter = _world
                .Filter<MoveToTargetActionComponent>()
                .Inc<TransformComponent>()
                .Inc<AiAgentComponent>()
                .Exc<ImmobilityComponent>()
                .Exc<DisabledComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var moveToTargetComponent = ref _moveToTargetPool.Get(entity);
                ref var directionComponent = ref _directionPool.Get(entity);
                ref var transformComponent = ref _transformPool.Get(entity);
                ref var targetComponentRequest = ref _movementPointPool.GetOrAddComponent(entity);
                
                var position = transformComponent.Position;
                var targetPosition = moveToTargetComponent.Position;
                var sqrDistance = math.distancesq(targetPosition,position);
                
                ref var destination = ref moveToTargetComponent.Position;
                ref var rotateToPoint = ref _rotateToPointPool.GetOrAddComponent(entity);
                rotateToPoint.Point = destination;
                
                if(_viewPool.Has(entity))
                    rotateToPoint.Point = directionComponent.Forward + position;
                
                if (sqrDistance < minSqrDistance)
                {
                    _world.AddComponent<MovementStopRequest>(entity);
                    continue;
                }
                
                targetComponentRequest.DestinationPosition = destination;
                var packedEntity = _world.PackEntity(entity);
                moveToTargetComponent.Effects.CreateRequests(_world,packedEntity,packedEntity);
            }
        }
    }
}
