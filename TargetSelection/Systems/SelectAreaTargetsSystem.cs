using Game.Code.GameLayers.Relationship;

namespace Game.Ecs.TargetSelection.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using Selection;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.Mathematics;
    
    /// <summary>
    /// select targets in area by radius
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class SelectAreaTargetsSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _targetFilter;

        private TargetSelectionAspect _targetAspect;
        private TargetSelectionSystem _targetSelection;
        
        private EcsPackedEntity[] _resultSelection = new EcsPackedEntity[TargetSelectionData.MaxTargets];

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _targetSelection = _world.GetGlobal<TargetSelectionSystem>();
            
            _targetFilter = _world
                .Filter<SqrRangeTargetsSelectionComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _targetFilter)
            {
                ref var selectionComponent = ref _targetAspect.TargetSelection.GetOrAddComponent(entity);
                var requests = selectionComponent.Requests;
                var results = selectionComponent.Results;

                for (int i = 0; i < requests.Length; i++)
                {
                    ref var request = ref requests[i];
                    if (request.Processed)
                    {
                        continue;
                    }
                    
                    var target = request.Target;
                    if (!target.Unpack(_world, out var targetEntity))
                    {
                        continue;
                    }

                    ref var result = ref results[i];
                    ref var transformComponent = ref _targetAspect.Position.Get(targetEntity);

                    var position = transformComponent.Position;
                    var radius = request.Radius;
                    var category = request.Category;
                    var layer = request.Relationship.GetFilterMask(request.SourceLayer);

                    var amount = _targetSelection.SelectEntitiesInArea(
                        _resultSelection,
                        radius,
                        ref position,
                        ref layer,
                        ref category);

                    result.Count = amount;
                    for (var j = 0; j < amount; j++)
                    {
                        ref var resultValue = ref _resultSelection[j];
                        result.Values[j] = resultValue;
                    }

                    result.Ready = true;
                    request.Processed = true;
                }
            }
        }
    }
    

}