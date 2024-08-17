namespace Game.Ecs.Movement.Systems.Converters
{
    using System;
    using Aspect;
    using Aspects;
    using Characteristics.Speed.Components;
    using Components;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.Di;
    using PrimeTween;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UnityEngine;

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
        private MovementAspect _navigationAspect;
        private MovementTweenAspect _tweenAspect;
        private MovementTweenTrackAspect _trackAspect;

        private EcsFilterInject<Inc<MovementTweenAgentComponent,
            MovementTweenDataComponent,
            TransformComponent,
            SpeedComponent>> _agentFilter;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _agentFilter.Value)
            {
                ref var movementAgent = ref _tweenAspect.Agent.Get(entity);
                ref var movementData = ref _tweenAspect.Tween.Get(entity);
                ref var transformComponent = ref _tweenAspect.Transform.Get(entity);
                ref var speedComponent = ref _tweenAspect.Speed.Get(entity);
                
                if(movementData.Sequence.isAlive) continue;
                
                if(!movementAgent.Track.Unpack(_world,out var track)) continue;
                
                var trackDataComponent = _trackAspect.Track.Get(track);

                ref var trackPoints = ref trackDataComponent.Points;
                if(trackPoints.Length == 0) continue;
                
                var sequence = Sequence.Create();
                var speed = speedComponent.Value;
                var transform = transformComponent.Value;
                transform.position = trackPoints[0].position;
                var previousPoint = trackPoints[0].position;
                
                for (var i = 1; i < trackPoints.Length; i++)
                {
                    var point = trackPoints[i];
                    var tween = Tween.PositionAtSpeed(transform,
                        previousPoint,
                        point.position, speed,ease: Ease.Linear);
                    
                    previousPoint = point.position;
                    sequence = sequence.Chain(tween);
                }
                
                movementData.Sequence = sequence;
                
                PlaySequence(sequence).Forget();
            }
        }
        
        private async UniTask PlaySequence(Sequence sequence)
        {
            await sequence;
        }
    }
}