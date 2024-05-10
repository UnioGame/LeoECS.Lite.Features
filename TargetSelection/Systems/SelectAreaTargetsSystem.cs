using Game.Code.GameLayers.Relationship;
using Game.Ecs.Core.Components;

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
        private EcsFilter _requestFilter;

        private TargetSelectionAspect _targetAspect;
        private TargetSelectionSystem _targetSelection;

        private EcsPool<OwnerComponent> _ownerPool;
        
        private EcsPackedEntity[] _resultSelection = new EcsPackedEntity[TargetSelectionData.MaxTargets];

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _targetSelection = _world.GetGlobal<TargetSelectionSystem>();
            
            _requestFilter = _world
                .Filter<SqrRangeTargetsSelectionRequestComponent>()
                .Inc<OwnerComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var requestEntity in _requestFilter)
            {
                ref var ownerComponent = ref _ownerPool.Get(requestEntity);
                if (!ownerComponent.Value.Unpack(_world, out var targetEntity))
                {
                    continue;
                }
                
                ref var requestComponent = ref _targetAspect.TargetSelectionRequest.GetOrAddComponent(requestEntity);
                ref var transformComponent = ref _targetAspect.Position.Get(targetEntity);
                var layer = requestComponent.Relationship.GetFilterMask(requestComponent.SourceLayer);
                var count = _targetSelection.SelectEntitiesInArea(
                    _resultSelection,
                    requestComponent.Radius,
                    ref transformComponent.Position,
                    ref layer,
                    ref requestComponent.Category);

                ref var resultComponent = ref _targetAspect.TargetSelectionResult.Get(targetEntity);
                var values = resultComponent.Results[requestComponent.ResultHash].Values;
                var result = new SqrRangeTargetSelectionResult
                {
                    Count = count,
                    Values = values
                };
                
                for (var j = 0; j < count; j++)
                {
                    ref var resultValue = ref _resultSelection[j];
                    result.Values[j] = resultValue;
                }

                result.Ready = true;
                resultComponent.Results[requestComponent.ResultHash] = result;
            }
        }
    }
    

}