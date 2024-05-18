namespace Game.Ecs.AI.Systems
{
    using System;
    using System.Collections.Generic;
    using Components;
    using Configurations;
    using Leopotam.EcsLite;
    using Data;

    [Serializable]
    public class AiCleanUpPlanningDataSystem : IEcsRunSystem, IEcsInitSystem
    {
        private IReadOnlyList<AiActionData> _actionData;
        private EcsFilter _filter;
        private EcsWorld _world;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AiAgentComponent>().End();
        }
        
        public AiCleanUpPlanningDataSystem(IReadOnlyList<AiActionData> actionData)
        {
            _actionData = actionData;
        }
    
        public void Run(IEcsSystems systems)
        {
            var agentComponentPool = _world.GetPool<AiAgentComponent>();
        
            foreach (var entity in _filter)
            {
                ref var agentComponent = ref agentComponentPool.Get(entity);
                agentComponent.PlannedActionsMask = ActionType.None;

                for (int i = 0; i < _actionData.Count; i++)
                {
                    var action = _actionData[i];
                    action.planner.RemoveComponent(systems, entity);
                    agentComponent.PlannerData[action.actionId] = new AiPlannerData
                    {
                        Priority = AiConstants.PriorityNever,
                    };
                }
            }
        }

    }
}
