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
    public sealed class CheckTargetMovementTweenSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private MovementAspect _movementAspect;
        private MovementTweenAspect _tweenAspect;
        private MovementTweenTrackAspect _trackAspect;

        private EcsFilterInject<Inc<MovementTweenAgentComponent,
            MovementTweenDataComponent>> _agentFilter;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _agentFilter.Value)
            {
                ref var movementData = ref _tweenAspect.Tween.Get(entity);
                
                var isComplete = movementData.IsCompleted;
                if (isComplete)
                {
                    _movementAspect.MovementTargetReached.TryRemove(entity);
                }
                else
                {
                    _movementAspect.MovementTargetReached.GetOrAddComponent(entity);
                }
            }
        }
    }
}