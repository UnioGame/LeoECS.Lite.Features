namespace Game.Ecs.AI.Components
{
    using System;
    using System.Collections.Generic;
    using Service;

    /// <summary>
    /// данные для плана агента
    /// </summary>
    [Serializable]
    public struct AiAgentPlanningComponent
    {
        public List<AiAgentPlanningData> AiPlan;

        public void AutoReset(ref AiAgentPlanningComponent c)
        {
            c.AiPlan?.Clear();
        }
    }

    /// <summary>
    /// данные компонента планирования
    /// </summary>
    [Serializable]
    public struct AiAgentPlanningData
    {
        public float Priority;
        public int ActionId;
    }

}
