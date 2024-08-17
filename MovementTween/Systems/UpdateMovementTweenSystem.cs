namespace Game.Ecs.Movement.Systems.Converters
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.Di;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

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
    public sealed class UpdateMovementTweenSystem : IEcsRunSystem
    {
        
        private EcsWorld _world;
        private MovementTweenAspect _tweenAspect;
        private MovementTweenTrackAspect _trackAspect;

        private EcsFilterInject<Inc<MovementTweenTrackComponent>> _trackFilter;
        private EcsFilterInject<Inc<MovementTweenAgentComponent>> _agentFilter;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _agentFilter.Value)
            {
                ref var movementAgent = ref _tweenAspect.Agent.Get(entity);
                if(movementAgent.Track.Unpack(_world,out var track)) continue;

                foreach (var trackEntity in _trackFilter.Value)
                {
                    ref var trackComponent = ref _trackAspect.Track.Get(trackEntity);
                    if(trackComponent.Id != movementAgent.TrackId) continue;
                    movementAgent.Track = _world.PackEntity(trackEntity);
                }
            }
        }
    }
}