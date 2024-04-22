namespace Game.Ecs.AI.Systems
{
    using System;
    using Components;
    using Leopotam.EcsLite;
    using Service;

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
                var actions = agentComponent.PlannedActions;

                //MaxPriorityPlanning(plan, actions);
                PriorityPlanning(plan, actions);
            }
        }

        private void PriorityPlanning(AiPlannerData[] plan,bool[] actions)
        {
            for (int i = 0; i < plan.Length; i++)
            {
                var priority = plan[i].Priority;
                actions[i] = priority>0;
            }
        }
        
        private void MaxPriorityPlanning(AiPlannerData[] plan,bool[] actions)
        {
            var maxPriority = -1f;
            var selectedId = -1;
            for (var i = 0; i < plan.Length; i++)
            {
                var priority = plan[i].Priority;
                if(priority<=maxPriority) continue;

                maxPriority = priority;
                selectedId = i;
            }

            for (var i = 0; i < plan.Length; i++)
                actions[i] = i == selectedId;
        }

    }
}
