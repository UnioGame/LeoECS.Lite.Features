namespace Game.Ecs.AI.Configurations
{
    using System;
    using Abstract;
    using Shared.Generated;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.Serialization;

    [Serializable]
    public class AiActionData
    {
        private const int LabelWidth = 100;
        
        [LabelWidth(LabelWidth)]
        [SerializeField]
        public ActionType actionId = ActionType.None;
        
        [LabelWidth(LabelWidth)]
        [InlineProperty]
        [SerializeReference]
        public IAiPlannerSystem planner;
        
        [LabelWidth(LabelWidth)]
        [InlineProperty]
        [SerializeReference]
        public IAiActionSystem action;
    }
}