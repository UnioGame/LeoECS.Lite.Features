namespace Game.Ecs.AI.Components
{
    using System;
    using System.Collections.Generic;
    using Configurations;
    using Leopotam.EcsLite;
    using Data;
    using Shared.Generated;
    using UnityEngine.Serialization;

    /// <summary>
    /// Компонент с данными агента необходимомыми для выполнения
    /// </summary>
    [Serializable]
    public struct AiAgentComponent
    {
        public AiAgentConfiguration Configuration;
        
        [FormerlySerializedAs("PlannedActions")]
        public ActionType PlannedActionsMask;
        public Dictionary<ActionType, AiPlannerData> PlannerData;
    }
}
