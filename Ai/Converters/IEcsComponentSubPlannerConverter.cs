namespace Game.Ecs.AI.Converters
{
    using Data;
    using Leopotam.EcsLite;

    public interface IEcsComponentSubPlannerConverter
    {
        public void Apply(EcsWorld world, int entity, ActionType actionId);
    }
}