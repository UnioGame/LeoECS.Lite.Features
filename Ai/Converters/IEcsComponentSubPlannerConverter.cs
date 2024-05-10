namespace Game.Ecs.AI.Converters
{
    using Leopotam.EcsLite;
    using Shared.Generated;

    public interface IEcsComponentSubPlannerConverter
    {
        public void Apply(EcsWorld world, int entity, ActionType actionId);
    }
}