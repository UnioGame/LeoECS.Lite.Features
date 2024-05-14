namespace Game.Ecs.AI.Systems
{
    using System;
    using System.Collections.Generic;
    using Components;
    using Configurations;
    using Leopotam.EcsLite;
    
    [Serializable]
    public class AiUpdatePlanningActionsStatusSystem : IEcsRunSystem, IEcsInitSystem
    {
        private IReadOnlyList<AiActionData> _actionData;
        private EcsFilter _filter;
        private EcsWorld _world;
        
        public AiUpdatePlanningActionsStatusSystem(IReadOnlyList<AiActionData> actionData)
        {
            _actionData = actionData;
        }

        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AiAgentComponent>().End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                var agentComponentPool = _world.GetPool<AiAgentComponent>();
                ref var agentComponent = ref agentComponentPool.Get(entity);
                for (int i = 0; i < _actionData.Count; i++)
                {
                    var action = _actionData[i];
                    if (!agentComponent.PlannedActionsMask.HasFlag(action.actionId))
                    {
                        action.planner.RemoveComponent(systems, entity);
                    }
                }
            }
        }

    }
}
