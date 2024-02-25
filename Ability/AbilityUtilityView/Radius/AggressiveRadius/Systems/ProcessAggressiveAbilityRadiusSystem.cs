namespace Game.Ecs.Ability.AbilityUtilityView.Radius.AggressiveRadius.Systems
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using AbilityUtilityView.Components;
    using Characteristics.Radius.Component;
    using Code.GameLayers.Category;
    using Code.GameLayers.Layer;
    using Common.Components;
    using Component;
    using Components;
    using Core;
    using Core.Components;
    using Core.Death.Components;
    using GameLayers.Category.Components;
    using GameLayers.Layer.Components;
    using Leopotam.EcsLite;
    using SubFeatures.Target.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.Runtime.ObjectPool.Extensions;
    using UnityEngine;
    using UnityEngine.Pool;

#if ENABLE_IL2CP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessAggressiveAbilityRadiusSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsFilter _categoryFilter;
        private EcsWorld _world;
        
        private EcsPool<TransformPositionComponent> _positionPool;
        private EcsPool<AggressiveRadiusViewDataComponent> _viewDataPool;
        private EcsPool<CategoryIdComponent> _gameCategoryPool;
        private EcsPool<LayerIdComponent> _layerPool;
        private EcsPool<AggressiveRadiusViewStateComponent> _statePool;
        private EcsPool<AbilityInHandLinkComponent> _abilityInHandLinkPool;
        private EcsPool<RadiusComponent> _radiusPool;
        private EcsPool<AbilityTargetsComponent> _chosenPool;
        private EcsPool<EntityAvatarComponent> _avatarPool;
        private EcsPool<ShowRadiusRequest> _showRadiusPool;
        private EcsPool<HideRadiusRequest> _hideRadiusPool;

        private List<int> _result = new();
        private List<int> _selectedDestinations = new();
        private List<EcsPackedEntity> _destinationsEntities = new();

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<AggressiveRadiusViewDataComponent>()
                .Inc<AggressiveRadiusViewStateComponent>()
                .Inc<TransformPositionComponent>()
                .Inc<VisibleUtilityViewComponent>()
                .Exc<DestroyComponent>()
                .End();
            
            _categoryFilter = _world
                .Filter<CategoryIdComponent>()
                .Inc<LayerIdComponent>()
                .End();
        }

        
        public void Run(IEcsSystems systems)
        {

            foreach (var entity in _filter)
            {
                ref var viewData = ref _viewDataPool.Get(entity);
                
                _result.Clear();
                _selectedDestinations.Clear();
                
                FindAllPossibleDestinations(
                    _result, 
                    _gameCategoryPool,
                    _layerPool, viewData.CategoryId,
                    viewData.LayerMask);

                ref var positionComponent = ref _positionPool.Get(entity);
                var ourPosition = positionComponent.Position;
                
                foreach (var destination in _result)
                {
                    if(!_positionPool.Has(destination)) continue;

                    if(!_abilityInHandLinkPool.Has(destination)) continue;
                    
                    if(!_avatarPool.Has(destination)) continue;

                    ref var abilityLink = ref _abilityInHandLinkPool.Get(destination);
                    if(!abilityLink.AbilityEntity.Unpack(_world, out var abilityEntity))
                        continue;

                    ref var abilityRadius = ref _radiusPool.Get(abilityEntity);
                    ref var destinationPosition = ref _positionPool.Get(destination);
                    ref var avatar = ref _avatarPool.Get(destination);
                    
                    var distance = EntityHelper.GetSqrDistance(
                        ref ourPosition, 
                        ref destinationPosition.Position,
                        ref avatar.Bounds);
                    
                    var noTargetDistance = abilityRadius.Value * abilityRadius.Value * 1.5f;
                    var closeTargetDistance = abilityRadius.Value * abilityRadius.Value * 1.2f;
                    var hasTargetDistance = abilityRadius.Value * abilityRadius.Value;

                    if (distance > noTargetDistance) continue;
                    
                    GameObject radiusView = null;
                    
                    if (distance < noTargetDistance)
                    {
                        radiusView = viewData.NoTargetRadiusView;
                    }

                    var hasChosenTargets = _chosenPool.Has(abilityEntity);
                    var chosenTargetCount = 0;
                    var isChosenUs = false;

                    if (hasChosenTargets)
                    {
                        ref var chosenTargets = ref _chosenPool.Get(abilityEntity);
                        chosenTargetCount = chosenTargets.Count;
                        isChosenUs = HasTargetWithId(_world, chosenTargets.Entities, entity);
                    }
                    
                    if (distance < closeTargetDistance)
                    {
                        if (hasChosenTargets && chosenTargetCount == 0)
                            radiusView = viewData.TargetCloseRadiusView;
                        else
                            radiusView = viewData.NoTargetRadiusView;
                    }
                    
                    if (distance < hasTargetDistance)
                    {
                        if(hasChosenTargets && isChosenUs)
                            radiusView = viewData.HasTargetRadiusView;
                        else
                            radiusView = viewData.NoTargetRadiusView;
                    }
                    
                    var showRequestEntity = _world.NewEntity();
                    ref var showRequest = ref _showRadiusPool.Add(showRequestEntity);
                
                    showRequest.Source = _world.PackEntity(entity);
                    showRequest.Destination = _world.PackEntity(destination);

                    showRequest.Radius = radiusView;
                    showRequest.Root = avatar.Feet;
                
                    var size = abilityRadius.Value * 2.0f;
                    showRequest.Size = new Vector3(size, size, size);
                    
                    _selectedDestinations.Add(destination);
                }
                
                ref var state = ref _statePool.Get(entity);

                _destinationsEntities.Clear();
                
                _world.PackAll(_destinationsEntities, _selectedDestinations);
                
                state.SetEntities(_destinationsEntities);
                
                foreach (var packedEntity in state.PreviousEntities)
                {
                    if(state.Entities.Contains(packedEntity)) continue;
                     
                    var hideRequestEntity = _world.NewEntity();
                    ref var hideRequest = ref _hideRadiusPool.Add(hideRequestEntity);

                    hideRequest.Source = _world.PackEntity(entity);
                    hideRequest.Destination = packedEntity;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool HasTargetWithId(EcsWorld world, EcsPackedEntity[] packedEntities, int entity)
        {
            foreach (var packedEntity in packedEntities)
            {
                if(!packedEntity.Unpack(world, out var targetEntity))
                    continue;

                if (targetEntity == entity)
                    return true;
            }

            return false;
        }

        private void FindAllPossibleDestinations(
            List<int> result,
            EcsPool<CategoryIdComponent> gameCategoryPool, 
            EcsPool<LayerIdComponent> layerMaskPool, 
            CategoryId categoryId, LayerId layerMask)
        {
            foreach (var entity in _categoryFilter)
            {
                ref var gameCategoryComponent = ref gameCategoryPool.Get(entity);
                if((gameCategoryComponent.Value & categoryId) != categoryId)
                    continue;

                ref var gameLayerComponent = ref layerMaskPool.Get(entity);
                if(!layerMask.HasFlag(ref gameLayerComponent.Value))
                    continue;
                
                result.Add(entity);
            }
        }
    }
}