namespace Game.Ecs.AI.Configurations
{
    using System;
    using System.Collections.Generic;
    using Abstract;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [Serializable]
    public class AiConfiguration
    {
        [Searchable(FilterOptions = SearchFilterOptions.ISearchFilterableInterface)]
        [ListDrawerSettings(ListElementLabelName = "@name")]
        public AiActionData[] aiActions = Array.Empty<AiActionData>();
    }
}