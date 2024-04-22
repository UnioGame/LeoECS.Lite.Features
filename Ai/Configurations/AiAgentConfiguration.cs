namespace Game.Ecs.AI.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abstract;
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

        public IReadOnlyList<AiAgentActionId> Actions => planners.Select(x => x.Id).ToList();

        public IReadOnlyList<IPlannerConverter> Planners => planners;
    }
}