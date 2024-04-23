namespace Game.Ecs.GameAi.ActivateAbility
{
    using System;
    using Ability.Aspects;
    using Ability.Tools;
    using AI.Components;
    using Code.Ai.ActivateAbility;
    using Code.Ai.ActivateAbility.Aspects;
    using Code.GameLayers.Relationship;
    using Components;
    using Core.Components;
    using Leopotam.EcsLite;
    using Selection;
    using TargetSelection;
    using UniGame.Core.Runtime;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.Collections;
    using Unity.Mathematics;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
    
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class SelectAbilityTargetsDataPlannerSystem : IEcsRunSystem , IEcsInitSystem
    {
        private AbilityTools _abilityTools;
        private TargetSelectionSystem _targetSelection;
        private AbilityAiActionAspect _aiActionAspect;
        private AbilityOwnerAspect _abilityOwnerAspect;
        private NativeHashSet<int> _entitySet;

        private EcsWorld _world;
        private EcsFilter _filter;
        private ILifeTime _lifeTime;

        private EcsPackedEntity[] _selection = new EcsPackedEntity[TargetSelectionData.MaxTargets];

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _lifeTime = _world.GetWorldLifeTime();
            _entitySet = new NativeHashSet<int>(10, Allocator.Persistent)
                .AddTo(_lifeTime);
            
            _targetSelection = _world.GetGlobal<TargetSelectionSystem>();
            _abilityTools = _world.GetGlobal<AbilityTools>();

            _filter = _world
                .Filter<AiAgentComponent>()
                .Inc<AbilityByRangeComponent>()
                .Inc<ActivateAbilityPlannerComponent>()
                .Exc<AbilityAiActionTargetComponent>()
                .Exc<PrepareToDeathComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                if(_abilityTools.GetNonDefaultAbilityInUse(entity) >= 0 ) continue;
                
                _entitySet.Clear();
                
                ref var rangeComponent = ref _aiActionAspect.ByRangeAbility.Get(entity);
                ref var gameLayerComponent = ref _aiActionAspect.LayerId.Get(entity);
                ref var entityTransformComponent = ref _aiActionAspect.Transform.Get(entity);
                ref var radiusComponent = ref _aiActionAspect.ByRangeAbility.Get(entity);

                var filters = rangeComponent.FilterData;
                
                foreach (var filter in filters)
                {
                    var abilitySlot = filter.Slot;
                    if(_entitySet.Contains(abilitySlot)) continue;

                    _entitySet.Add(abilitySlot);
                    
                    var abilityEntity = _abilityTools.GetAbilityBySlot(entity, abilitySlot);
                    if(abilityEntity <= 0) continue;
                    
                    ref var dataComponent = ref _aiActionAspect
                        .AbilityRangeData
                        .GetOrAddComponent(abilityEntity);

                    ref var filterComponent = ref _aiActionAspect.Relationship.Get(abilityEntity);
                    ref var categoryIdComponent = ref _aiActionAspect.Category.Get(abilityEntity);

                    var entityPosition = (float3)entityTransformComponent.Value.position;

                    //create relation mask by gamelayer id
                    var gameLayerMask = filterComponent.Value.GetFilterMask(gameLayerComponent.Value);

                    var amount = _targetSelection.SelectEntitiesInArea(
                        _selection,
                        radiusComponent.Radius,
                        ref entityPosition,
                        ref gameLayerMask,
                        ref categoryIdComponent.Value);

                    dataComponent.Count = amount;
                    
                    for (var i = 0; i < amount; i++)
                        dataComponent.Values[i] = _selection[i];
                }
            }
        }
    }
}