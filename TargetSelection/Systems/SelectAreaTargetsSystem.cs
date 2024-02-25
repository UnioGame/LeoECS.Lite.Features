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
                .Filter<SqrRangeFilterTargetComponent>()
                .Inc<SqrRangeTargetSelectionComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _targetFilter)
            {
                ref var targetComponent = ref _targetAspect.Data.Get(entity);
                ref var resultComponent = ref _targetAspect.Result.GetOrAddComponent(entity);
                
                var target = targetComponent.Target;
                if(!target.Unpack(_world,out var targetEntity)) continue;
                
                ref var transformComponent = ref _targetAspect.Position.Get(targetEntity);

                var position = transformComponent.Position;
                var radius = targetComponent.Radius;
                var category = targetComponent.Category;
                var layer = targetComponent.Layer;
                
                var amount = _targetSelection.SelectEntitiesInArea(
                    _resultSelection,
                    radius,
                    ref position,
                    ref layer,
                    ref category);

                resultComponent.Count = amount;
                
                for (var i = 0; i < amount; i++)
                {
                    ref var resultValue = ref _resultSelection[i];
                    resultComponent.Values[i] = resultValue;
                }
            }
        }
    }
    

}