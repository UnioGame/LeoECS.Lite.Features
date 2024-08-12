namespace Game.Ecs.GameResources.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.Di;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ProcessSpawnRequestSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private GameResourceTaskAspect _taskAspect;
    
        private EcsFilterInject<Inc<GameResourceSpawnRequest>> _filter;
        
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var spawnRequest = ref _taskAspect.SpawnRequest.Get(entity);
                var taskEntity = _world.NewEntity();
                
                ref var gameResourceComponent = ref _taskAspect.Handle.Add(taskEntity);
                ref var targetComponent = ref _taskAspect.Target.Add(taskEntity);
                ref var parentEntity = ref _taskAspect.ParentEntity.Add(taskEntity);
                    
                gameResourceComponent.Resource = spawnRequest.ResourceId;
                gameResourceComponent.Source = spawnRequest.Source;
                gameResourceComponent.Owner = spawnRequest.Owner;
                gameResourceComponent.LifeTime = spawnRequest.LifeTime;
                
                parentEntity.Value = spawnRequest.ParentEntity;
                targetComponent.Value = spawnRequest.Entity;
                
                
                ref var positionComponent = ref _taskAspect.Position.Add(taskEntity);
                ref var rotationComponent = ref _taskAspect.Rotation.Add(taskEntity);
                ref var scaleComponent = ref _taskAspect.Scale.Add(taskEntity);
                ref var parentComponent = ref _taskAspect.Parent.Add(taskEntity);
            
                parentComponent.Value = spawnRequest.Parent;
                positionComponent.Value = spawnRequest.LocationData.Position;
                rotationComponent.Value = spawnRequest.LocationData.Rotation;
                scaleComponent.Value = spawnRequest.LocationData.Scale;
            }
        }
    }
}