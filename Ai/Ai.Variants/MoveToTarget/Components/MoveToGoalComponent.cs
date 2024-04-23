namespace Game.Ecs.GameAi.MoveToTarget.Components
{
    using System.Collections.Generic;
    using Data;
    using Leopotam.EcsLite;

    public struct MoveToGoalComponent : IEcsAutoReset<MoveToGoalComponent>
    {
        public List<MoveToGoalData> Goals;
        
        public void AutoReset(ref MoveToGoalComponent c)
        {
            c.Goals ??= new List<MoveToGoalData>(8);
            c.Goals.Clear();
        }
    }
}