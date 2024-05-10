namespace Game.Ecs.AI.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abstract;
    using Shared.Generated;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.Serialization;

    [Serializable]
    public class AiAgentConfiguration
    {
        #region inspector

        [FormerlySerializedAs("_planners")]
        [SerializeReference]
        [InlineProperty]
        public List<PlannerConverter> planners = new List<PlannerConverter>();

        #endregion

        public IReadOnlyList<ActionType> Actions => planners.Select(x => x.ActionId).ToList();

        public IReadOnlyList<IPlannerConverter> Planners => planners;
    }
}