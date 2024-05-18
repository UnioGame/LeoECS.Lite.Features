namespace Ai.Ai.Variants.Prioritizer.Data
{
    using System;
    using Converters;
    using UnityEngine;

    [Serializable]
    public class PrioritizerConfig
    {
        [SerializeField]
        public TargetPrioritizerConverter prioritizerConverter;
    }
}