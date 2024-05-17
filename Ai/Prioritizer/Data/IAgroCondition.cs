namespace Ai.Ai.Variants.Prioritizer.Data
{
    using Leopotam.EcsLite;

    public interface IAgroCondition
    {
        public bool Check(EcsWorld world, int source, int target);
    }
}