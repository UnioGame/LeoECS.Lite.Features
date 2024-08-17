namespace Game.Ecs.Movement.Systems.Converters
{
    using System;
    using Aspect;
    using Aspects;
    using Characteristics.Speed.Components;
    using Components;
    using Core.Components;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.Di;
    using PrimeTween;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// Система отвечающая за конвертацию вектора скорости в следующую позицию для перемещения через систему NavMesh.
    /// </summary>
    #if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ActivateMovementTweenSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private MovementAspect _movementAspect;
        private MovementTweenAspect _tweenAspect;
        private MovementTweenTrackAspect _trackAspect;

        private EcsFilterInject<Inc<MovementTweenAgentComponent,
            MovementTweenDataComponent,
            TransformComponent,
            SpeedComponent>,
            Exc<ImmobilityComponent,PrepareToDeathComponent>> _agentFilter;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _agentFilter.Value)
            {
                ref var movementAgent = ref _tweenAspect.Agent.Get(entity);
                ref var movementData = ref _tweenAspect.Tween.Get(entity);
                ref var transformComponent = ref _tweenAspect.Transform.Get(entity);
                ref var speedComponent = ref _tweenAspect.Speed.Get(entity);
                
                if(movementData.Tween.isAlive) continue;
                
                if(!movementAgent.Track.Unpack(_world,out var track)) continue;
                
                var trackDataComponent = _trackAspect.Track.Get(track);
                var pointIndex = movementData.Index;
                
                ref var trackPoints = ref trackDataComponent.Points;
                var pointsCount = trackPoints.Length;
                
                if(pointsCount == 0 || pointIndex>=pointsCount) continue;
                
                var speed = speedComponent.Value;
                var transform = transformComponent.Value;
                
                transform.position = trackPoints[pointIndex].position;
                var previousPoint = trackPoints[pointIndex].position;
                var nextPointIndex = pointIndex + 1;
                var isComplete = nextPointIndex >= pointsCount;
                
                nextPointIndex = isComplete ? pointIndex : nextPointIndex;
                
                var point = trackPoints[nextPointIndex];
                var lastPoint = trackPoints[pointsCount - 1];
                var nextPoint = point.position;
                
                var tween = Tween.PositionAtSpeed(transform,
                    previousPoint,
                    nextPoint, speed,ease: Ease.Linear);
                
                movementData.Tween = tween;
                movementData.Index = nextPointIndex;
                movementData.IsCompleted = isComplete;
                
                ref var rotateRequest = ref _tweenAspect.RotateTo.GetOrAddComponent(entity);
                rotateRequest.Point = nextPoint;
                
                ref var movementTarget = ref _movementAspect.Target.GetOrAddComponent(entity);
                movementTarget.Value = lastPoint.position;
                
                PlaySequence(tween).Forget();
            }
        }
        
        private async UniTask PlaySequence(Tween tween)
        {
            await tween;
        }
    }
}