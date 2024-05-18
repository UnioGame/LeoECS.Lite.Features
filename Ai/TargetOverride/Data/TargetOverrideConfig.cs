namespace Game.Ecs.AI.TargetOverride.Data
{
    using System;
    using Converters;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [Serializable]
    public class TargetOverrideConfig
    {
        [InlineProperty]
        [SerializeReference]
        public ITargetOverrideConverter[] Converters;
    }
}