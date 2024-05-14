namespace Game.Ecs.AI.Systems
{
    using System;
    using System.Collections.Generic;
    using Components;
    using Leopotam.EcsLite;
    using Data;

    [Serializable]
    public class AiPlanningSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AiAgentComponent>()
                .Exc<AiAgentSelfControlComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var aiAgentComponentPool = _world.GetPool<AiAgentComponent>();

            foreach (var entity in _filter)
            {
                ref var agentComponent = ref aiAgentComponentPool.Get(entity);
                var plan = agentComponent.PlannerData;
                ref var actions = ref agentComponent.PlannedActionsMask;

                MaxPriorityPlanning(plan, ref actions);
                //PriorityPlanning(plan, actions);
            }
        }

        private void PriorityPlanning(AiPlannerData[] plan, bool[] actions)
        {
            for (int i = 0; i < plan.Length; i++)
            {
                var priority = plan[i].Priority;
                actions[i] = priority > 0;
            }
        }
        
        private void MaxPriorityPlanning(Dictionary<ActionType, AiPlannerData> plan, ref ActionType plannedActionsMask)
        {
            var maxPriority = AiConstants.PriorityNever;
            ActionType selectedId = ActionType.None;
            foreach (var key in plan.Keys)
            {
                var priority = plan[key].Priority;
                if (priority <= maxPriority)
                {
                    continue;
                }

                maxPriority = priority;
                selectedId = key;
            }

            plannedActionsMask = selectedId;
        }

    }
}
