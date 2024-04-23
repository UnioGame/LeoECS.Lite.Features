namespace Game.Ecs.AI.Configurations
{
    using System;
    using Abstract;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.Serialization;

    [Serializable]
    public class AiActionData : ISearchFilterable
    {
        private const int LabelWidth = 100;
        
        [FormerlySerializedAs("_name")]
        [LabelWidth(LabelWidth)]
        [SerializeField]
        public string name = string.Empty;
        
        [FormerlySerializedAs("_planner")]
        [LabelWidth(LabelWidth)]
        [InlineProperty]
        [SerializeReference]
        public IAiPlannerSystem planner;

        [FormerlySerializedAs("_action")]
        [LabelWidth(LabelWidth)]
        [InlineProperty]
        [SerializeReference]
        public IAiActionSystem action;

        public bool IsMatch(string searchString)
        {
            if (string.IsNullOrEmpty(searchString)) return true;
            if (name.IndexOf(searchString,StringComparison.OrdinalIgnoreCase) >= 0) return true;

            var typeName = planner?.GetType().Name;
            if (typeName != null && typeName.IndexOf(searchString,StringComparison.OrdinalIgnoreCase) >= 0) return true;
            
            typeName = action?.GetType().Name;
            if (typeName != null && typeName.IndexOf(searchString,StringComparison.OrdinalIgnoreCase) >= 0) return true;

            return false;
        }
    }
}