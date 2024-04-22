namespace Game.Ecs.AI.Abstract
{
    using System.Collections.Generic;
    using Configurations;

    public interface IAiConfiguration
    {
        IReadOnlyList<AiActionData> AiActions { get; }
    }
}