namespace Game.Ecs.Selection
{
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Code.GameLayers.Category;
    using Code.GameLayers.Layer;
    using Core;
    using Core.Components;
    using Core.Death.Components;
    using GameLayers.Category.Components;
    using GameLayers.Layer.Components;
    using Leopotam.EcsLite;
    using TargetSelection;
    using TargetSelection.Aspects;
    using TargetSelection.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using Unity.Mathematics;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [ECSDI]
    public class TargetSelectionSystem : IEcsInitSystem,IEcsDestroySystem
    {
        private EntityFloat[] _selectionBuffer = new EntityFloat[TargetSelectionData.MaxTargets];
        private EcsPackedEntity[] _packedBuffer = new EcsPackedEntity[TargetSelectionData.MaxTargets];
        private int[] _entitiesBuffer = new int[TargetSelectionData.MaxTargets];
        
        private TargetSelectionAspect _targetSelectionAspect;
        private TargetAspect _targetAspect;
        private EcsWorld _world;
        
        private EcsFilter _kdDataFilter;
        
        private List<int> _queryResult = new(TargetSelectionData.MaxAgents);
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _kdDataFilter = _world
                .Filter<KDTreeDataComponent>()
                .Inc<KDTreeComponent>()
                .Inc<KDTreeQueryComponent>()
                .End();
        }
                
                
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int SelectEntitiesInArea(
            int[] resultContainer, 
            float radius, 
            ref float3 sourcePosition, 
            ref LayerId layerId, 
            ref CategoryId categoryId)
        {
            var result = resultContainer;
            var counter = 0;
            
            var amount = SelectEntitiesInArea(_entitiesBuffer,_packedBuffer,radius, ref sourcePosition);
            amount = math.min(TargetSelectionData.MaxTargets, amount);
            
            for (var i = 0; i < amount; i++)
            {
                var entity = _entitiesBuffer[i];
                if(!_targetSelectionAspect.Layer.Has(entity)) continue;
                
                ref var layerComponent = ref _targetSelectionAspect.Layer.Get(entity);
                ref var categoryComponent = ref _targetSelectionAspect.Category.Get(entity);

                var layer = layerComponent.Value;
                var category = categoryComponent.Value;

                if ((layerId & layer) != layer ||
                    (categoryId & category) != category) continue;
                
                result[counter] = entity;
                counter++;
            }

            return counter;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int SelectEntitiesInArea(
            EcsPackedEntity[] result, 
            float radius, 
            ref float3 sourcePosition, 
            ref LayerId layerId, 
            ref CategoryId categoryId)
        {
            var counter = 0;
            
            var amount = SelectEntitiesInArea(_entitiesBuffer,_packedBuffer,radius, ref sourcePosition);
            amount = math.min(TargetSelectionData.MaxTargets, amount);
            
            for (var i = 0; i < amount; i++)
            {
                var entity = _entitiesBuffer[i];
                ref var packedEntity = ref _packedBuffer[i];
                
                ref var layerComponent = ref _targetSelectionAspect.Layer.Get(entity);
                ref var categoryComponent = ref _targetSelectionAspect.Category.Get(entity);

                var layer = layerComponent.Value;
                var category = categoryComponent.Value;

                if ((layerId & layer) != layer ||
                    (categoryId & category) != category) continue;

                if(entity < 0) continue;
                
                result[counter] = packedEntity;
                counter++;
            }

            return counter;
        }
        
        public int SelectEntitiesInArea(
            int[] result, 
            EcsPackedEntity[] packedResult,
            float radius,
            ref float3 sourcePosition)
        {
            foreach (var kdEntity in _kdDataFilter)
            {
                _queryResult.Clear();
                
                ref var dataComponent = ref _targetAspect.Data.Get(kdEntity);
                ref var kdTreeComponent = ref _targetAspect.Tree.Get(kdEntity);
                ref var queryComponent = ref _targetAspect.Query.Get(kdEntity);

                var tree = kdTreeComponent.Value;
                var query = queryComponent.Value;
                
                query.Radius(tree,sourcePosition,radius,_queryResult);
                
                var amount = _queryResult.Count;
                amount = math.min(TargetSelectionData.MaxTargets, amount);
                
                var counter = 0;
                for (var i = 0; i < amount; i++)
                {
                    var index = _queryResult[i];
                    ref var packed = ref dataComponent.PackedEntities[index];
                    if(!packed.Unpack(_world, out var entity))
                       continue;
                    
                    packedResult[counter] = packed;
                    result[counter] = entity;
                    counter++;
                }

                return counter;
            }

            return 0;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int IsPassedByMask(
            ref EcsPackedEntity packedEntity,
            ref LayerId layerId, 
            ref CategoryId categoryId)
        {
            if (!packedEntity.Unpack(_world, out var entity))
                return -1;
            
            ref var layerComponent = ref _targetSelectionAspect.Layer.Get(entity);
            ref var categoryComponent = ref _targetSelectionAspect.Category.Get(entity);

            var layer = layerComponent.Value;
            var category = categoryComponent.Value;

            if ((layerId & layer) != layer ||
                (categoryId & category) != category) return -1;

            return entity;
        }
        

        public void Destroy(IEcsSystems systems)
        {
            
        }
    }
    
}