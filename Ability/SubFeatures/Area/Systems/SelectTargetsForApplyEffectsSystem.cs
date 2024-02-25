namespace Game.Ecs.Ability.SubFeatures.Area.Systems
{
    using System;
    using Aspects;
    using Code.GameLayers.Relationship;
    using Common.Components;
    using Components;
    using Core.Components;
    using Ecs.Selection;
    using GameLayers.Category.Components;
    using GameLayers.Relationship.Components;
    using Leopotam.EcsLite;
    using Target.Components;
    using TargetSelection;
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
    public sealed class SelectTargetsForApplyEffectsSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        private AreaAspect _areaAspect;

        private TargetSelectionSystem _targetSelection;
        
        private EcsPackedEntity[] _entityPoints = new EcsPackedEntity[TargetSelectionData.MaxTargets];

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _targetSelection = _world.GetGlobal<TargetSelectionSystem>();

            _filter = _world
                .Filter<ApplyAbilityEffectsSelfRequest>()
                .Inc<AreableAbilityComponent>()
                .Inc<AreaLocalPositionComponent>()
                .Inc<AreaRadiusComponent>()
                .Inc<OwnerComponent>()
                .Inc<AbilityTargetsComponent>()
                .Inc<RelationshipIdComponent>()
                .Inc<CategoryIdComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var owner = ref _areaAspect.Owner.Get(entity);
                if (!owner.Value.Unpack(_world, out var ownerEntity))
                    continue;

                ref var ownerTransform = ref _areaAspect.Position.Get(ownerEntity);
                var ownerPosition = ownerTransform.Position;

                ref var ownerLayerId = ref _areaAspect.Layer.Get(ownerEntity);
                ref var areaPosition = ref _areaAspect.AreaPosition.Get(entity);
                ref var areaRadius = ref _areaAspect.AreaRadius.Get(entity);
                ref var relationship = ref _areaAspect.Relationship.Get(entity);
                ref var category = ref _areaAspect.Category.Get(entity);

                var position = ownerPosition + areaPosition.Value;
                var layerId = relationship.Value.GetFilterMask(ownerLayerId.Value);

                var amount = _targetSelection.SelectEntitiesInArea(
                    _entityPoints,
                    areaRadius.Value,
                    ref position,
                    ref layerId,
                    ref category.Value);

                ref var targets = ref _areaAspect.Targets.Get(entity);
                targets.SetEntities(_entityPoints, amount);
            }
        }
    }
}