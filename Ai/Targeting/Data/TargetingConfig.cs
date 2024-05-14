namespace Game.Ecs.AI.Targeting.Data
{
    using System;
    using Ai.Targeting.Converters;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [Serializable]
    public class TargetingConfig
    {
        [SerializeReference]
        [InlineProperty]
        public ITargetSelectorConverter[] targetSelectors;
    }
}