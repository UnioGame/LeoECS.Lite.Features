namespace Game.Ecs.AI.Components
{
    using System;
    using System.Collections.Generic;
    using Configurations;
    using Data;
    using UnityEngine.Serialization;
    
    [Serializable]
    public struct AiAgentComponent
    {
        public AiAgentConfiguration Configuration;
        
        [FormerlySerializedAs("PlannedActions")]
        public ActionType PlannedActionsMask;
        public Dictionary<ActionType, AiPlannerData> PlannerData;
    }
}
