namespace Ai.Ai.Variants.Prioritizer.Data
{
    using Leopotam.EcsLite;

    public interface IPriorityComparer
    {
        public int Compare(EcsWorld world, int source, int current, int potential);
    }
}