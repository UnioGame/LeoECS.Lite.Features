namespace Game.Ecs.Movement.Systems.Converters
{
    using System;
    using Aspect;
    using Aspects;
    using Components;
    using Core.Components;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.Di;
    using PrimeTween;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class StopMovementTweenSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private MovementAspect _movementAspect;
        private MovementTweenAspect _tweenAspect;
        private MovementTweenTrackAspect _trackAspect;

        private EcsFilterInject<Inc<MovementTweenAgentComponent,
            MovementTweenDataComponent,
            ImmobilityComponent>> _agentFilter;
        
        private EcsFilterInject<Inc<MovementTweenAgentComponent,
            PrepareToDeathComponent>> _readyToDeathFilter;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _readyToDeathFilter.Value)
            {
                _movementAspect.Immobility.GetOrAddComponent(entity);
            }
            
            foreach (var entity in _agentFilter.Value)
            {
                ref var movementData = ref _tweenAspect.Tween.Get(entity);
                if(movementData.Tween.isAlive) 
                    movementData.Tween.Stop();
            }
        }
    }
}