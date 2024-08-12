namespace Game.Ecs.GameResources.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.Di;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.Runtime.ObjectPool.Extensions;
    using UnityEngine;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class CreateSpawnObjectSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        
        private GameResourceTaskAspect _taskAspect;
        private GameResourceAspect _resourceAspect;
     
        private EcsFilterInject<
            Inc<GameResourceResultComponent,GameResourceTaskComponent>,
            Exc<GameResourceTaskCompleteComponent>> _filter;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _taskAspect.Complete.Add(entity);
                
                ref var resourceComponent = ref _taskAspect.Result.Get(entity);
                var resource = resourceComponent.Resource;
                
                if(resource == null) continue;

                ref var handleComponent = ref _taskAspect.Handle.Get(entity);
                ref var positionComponent = ref _taskAspect.Position.Get(entity);
                ref var parentComponent = ref _taskAspect.Parent.Get(entity);
                ref var rotationComponent = ref _taskAspect.Rotation.Get(entity);
                ref var targetComponent = ref _taskAspect.Target.Get(entity);
                ref var scaleComponent = ref _taskAspect.Scale.Get(entity);
                ref var parentEntityComponent = ref _taskAspect.ParentEntity.Get(entity);
                
                var parent = parentComponent.Value;
                var targetPosition = positionComponent.Value;
                var rotation = rotationComponent.Value;
                var instance = resource.Spawn(targetPosition, rotation, parent);
                              
                if (instance == null) continue;
                
                //non gameobject case
                if (instance is not GameObject && instance is not Component) continue;
                
                var targetGameObject = instance is Component component
                    ? component.gameObject 
                    : instance as GameObject;
                
                var pawnEntity = -1;
                if (targetComponent.Value.Unpack(_world, out var target))
                    pawnEntity = target;
                
                pawnEntity = pawnEntity == -1 ? _world.NewEntity() : pawnEntity;

                ref var sourceLinkComponent = ref _resourceAspect.SourceLink.Add(pawnEntity);
                sourceLinkComponent.Source = handleComponent.Source;
                sourceLinkComponent.SpawnedEntity = _world.PackEntity(pawnEntity);
                
                if (handleComponent.Owner.Unpack(_world, out var ownerEntity))
                {
                    ref var pawnOwnerComponent = ref _resourceAspect.Owner.Add(pawnEntity);
                    pawnOwnerComponent.Value = handleComponent.Owner;
                }

                if (parentEntityComponent.Value.Unpack(_world, out var parentEntity))
                {
                    ref var pawnParentEntityComponent = ref _resourceAspect.Parent.Add(pawnEntity);
                    pawnParentEntityComponent.Value = parentEntityComponent.Value;
                }

                if (target > 0) _resourceAspect.Target.Add(pawnEntity);
                
                ref var pawnSpawnedComponent = ref _resourceAspect.SpawnedResource.Add(pawnEntity);
                ref var pawnObjectComponent = ref _resourceAspect.Object.Add(pawnEntity);
                ref var pawnResourceComponent = ref _resourceAspect.Resource.GetOrAddComponent(pawnEntity);
                
                pawnResourceComponent.Value = handleComponent.Resource;
                pawnSpawnedComponent.Source = handleComponent.Source;
                pawnObjectComponent.Value = instance;
                
                if(targetGameObject == null) continue;
                
                //targetGameObject.transform.localScale = scaleComponent.Value;
                ref var pawnGameObjectComponent = ref _resourceAspect.GameObject.Add(pawnEntity);
                pawnGameObjectComponent.Value = targetGameObject;
            }

        }

    }
}