namespace Game.Ecs.Ability.SubFeatures.Selection.UserInput.Systems
{
    using System;
    using Characteristics.Radius.Component;
    using Code.GameLayers.Relationship;
    using Common.Components;
    using Components;
    using Core.Components;
    using Ecs.Selection;
    using GameLayers.Category.Components;
    using GameLayers.Layer.Components;
    using GameLayers.Relationship.Components;
    using Leopotam.EcsLite;
    using TargetSelection;
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
    public sealed class SelectTargetsSystem : IEcsRunSystem,IEcsInitSystem
    {
        private readonly float _radiusMultiplier;
        private EcsFilter _filter;
        private EcsWorld _world;

        private TargetSelectionSystem _selectionSystem;
        private int[] _selection = new int[TargetSelectionData.MaxTargets];
        private EcsPackedEntity[] _packedEntities = new EcsPackedEntity[TargetSelectionData.MaxTargets];
        
        private EcsPool<SelectedTargetsComponent> _targetsPool;
        private EcsPool<RadiusComponent> _radiusPool;
        private EcsPool<RelationshipIdComponent> _relationshipPool;
        private EcsPool<CategoryIdComponent> _categoryPool;
        private EcsPool<OwnerComponent> _ownerPool;
        private EcsPool<TransformComponent> _transformPool;
        private EcsPool<TransformPositionComponent> _positionPool;
        private EcsPool<LayerIdComponent> _layerPool;
        private EcsPool<PrepareToDeathComponent> _prepareToDeath;

        public SelectTargetsSystem(float radiusMultiplier = 1.5f) // TODO: to global config?
        {
            _radiusMultiplier = radiusMultiplier;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _selectionSystem = _world.GetGlobal<TargetSelectionSystem>();
            
            _filter = _world
                .Filter<AbilityInHandComponent>()
                .Inc<SelectableAbilityComponent>()
                .Inc<RelationshipIdComponent>()
                .Inc<CategoryIdComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var owner = ref _ownerPool.Get(entity);
                if (!owner.Value.Unpack(_world, out var ownerEntity))
                    continue;

                ref var targets = ref _targetsPool.GetOrAddComponent(entity);

                if (_prepareToDeath.Has(ownerEntity))
                {
                    targets.SetEntities(Array.Empty<EcsPackedEntity>(),0);
                    continue;
                }

                ref var relationship = ref _relationshipPool.Get(entity);
                if (relationship.Value.IsSelf())
                    targets.SetEntity(owner.Value);

                ref var radius = ref _radiusPool.Get(entity);

                ref var ownerPosition = ref _positionPool.Get(ownerEntity);
                ref var ownerLayerMask = ref _layerPool.Get(ownerEntity);
                
                var areaCenter = ownerPosition.Position;
                var searchRadius = radius.Value * _radiusMultiplier;

                ref var category = ref _categoryPool.Get(entity);
                var layer = relationship.Value.GetFilterMask(ownerLayerMask.Value);
                
                var amount = _selectionSystem.SelectEntitiesInArea(
                    _selection,
                    searchRadius, 
                    ref areaCenter,
                    ref layer, 
                    ref category.Value);

                for (int i = 0; i < amount; i++)
                    _packedEntities[i] = _world.PackEntity(_selection[i]);
                
                targets.SetEntities(_packedEntities,amount);
            }
        }
    }
}