namespace Game.Ecs.AI.Configurations
{
    using System;
    using Sirenix.OdinInspector;

    [Serializable]
    public class AiConfiguration
    {
        [Searchable(FilterOptions = SearchFilterOptions.ISearchFilterableInterface)]
        public AiActionData[] aiActions = Array.Empty<AiActionData>();
    }
}