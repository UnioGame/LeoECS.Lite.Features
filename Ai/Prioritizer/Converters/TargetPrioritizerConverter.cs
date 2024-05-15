namespace Ai.Ai.Variants.Prioritizer.Converters
{
    using System;
    using Components;
    using Data;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;

    [Serializable]
    public class TargetPrioritizerConverter : IPrioritizerConverter
    {
        public PrioritizerComponent prioritizer;

        public void Apply(EcsWorld world, int entity)
        {
            ref var prioritizerComponent = ref world.AddComponent<PrioritizerComponent>(entity);
            prioritizerComponent.Comparers = new IPriorityComparer[prioritizer.Comparers.Length];
            for (int i = 0; i < prioritizer.Comparers.Length; i++)
            {
                prioritizerComponent.Comparers[i] = prioritizer.Comparers[i];
            }
        }
    }
}