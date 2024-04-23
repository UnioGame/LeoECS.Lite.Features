namespace Game.Ecs.GameAi.MoveToTarget.Components
{
    using System.Collections.Generic;
    using Data;
    using Leopotam.EcsLite;

    public struct MoveToPoiGoalsComponent : IEcsAutoReset<MoveToPoiGoalsComponent>
    {
        public Dictionary<int, MoveToGoalData> GoalsLinks;

        public void AutoReset(ref MoveToPoiGoalsComponent c)
        {
            c.GoalsLinks ??= new Dictionary<int, MoveToGoalData>(8);
            c.GoalsLinks.Clear();
        }
    }
}