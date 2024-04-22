namespace Game.Ecs.GameAi.MoveToTarget.Systems
{
    using System;
    using Code.GameLayers.Category;
    using Data;
    using Components;
    using Core.Death.Components;
    using Game.Ecs.GameLayers.Category.Components;
    using Game.Ecs.GameLayers.Layer.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using Unity.Mathematics;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class SelectPoiPlannerSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsFilter _poiFilter;
        private EcsPool<MoveByPoiComponent> _dataPool;
        private EcsPool<MoveToPoiComponent> _poiPool;
        private EcsPool<MoveToPoiGoalsComponent> _goalsPool;
        private EcsPool<MoveToGoalComponent> _moveGoalPool;
        private EcsPool<TransformPositionComponent> _transformPool;
        private EcsPool<CategoryIdComponent> _categoryPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<MoveByPoiComponent>()
                .Inc<MoveToTargetPlannerComponent>()
                .Inc<MoveToPoiGoalsComponent>()
                .Inc<MoveToGoalComponent>()
                .Inc<TransformPositionComponent>()
                .Inc<LayerIdComponent>()
                .Inc<CategoryIdComponent>()
                .Exc<DisabledComponent>()
                .End();
            
            _poiFilter = _world
                .Filter<MoveToPoiComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var dataComponent = ref _dataPool.Get(entity);
                ref var categoryComponent = ref _categoryPool.Get(entity);
                ref var goalsComponent = ref _goalsPool.Get(entity);
                ref var transformComponent = ref _transformPool.Get(entity);
                ref var goals = ref _moveGoalPool.Get(entity);

                //Если уже есть найденные цели, то POI игнорируются
                var goalsTargets = goals.Goals;
                if (goalsTargets.Count > 0)
                    continue;

                var sqrRange = dataComponent.ReachRange * dataComponent.ReachRange;
                var position = transformComponent.Position;
                var poiGoals = goalsComponent.GoalsLinks;
                var minDistance = float.MaxValue;
                var targetEntity = -1;
                var targetGoal = new MoveToGoalData();

                foreach (var key in _poiFilter)
                {
                    var poiComponent = _poiPool.Get(key);

                    if (poiComponent.Priority < 0)
                        continue;

                    var complete = false;
                    if (poiGoals.TryGetValue(key, out var value))
                        complete = value.Complete;

                    if (complete) continue;

                    var targetCategory = poiComponent.CategoryId;
                    if (!targetCategory.HasFlag(categoryComponent.Value)) continue;

                    var targetPosition = poiComponent.Position;
                    var distance = math.distancesq(position, targetPosition);
                    
                    if (distance >= minDistance) continue;

                    minDistance = distance;
                    targetEntity = key;

                    value.Complete = distance < sqrRange;
                    value.Position = targetPosition;
                    value.Priority = poiComponent.Priority;
                    value.Target = _world.PackEntity(key);

                    targetGoal = value;
                    poiGoals[key] = targetGoal;
                }

                if (targetEntity < 0) continue;

                goals.Goals.Add(targetGoal);
            }
        }
    }
}