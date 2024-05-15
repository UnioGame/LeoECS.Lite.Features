namespace Game.Ecs.AI.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abstract;
    using Data;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [Serializable]
    public class AiAgentConfiguration
    {
        #region inspector
        
        [SerializeReference]
        [InlineProperty]
        public List<PlannerConverter> planners = new List<PlannerConverter>();

        #endregion

        public IReadOnlyList<ActionType> Actions => planners.Select(x => x.ActionId).ToList();

        public IReadOnlyList<IPlannerConverter> Planners => planners;
    }
}