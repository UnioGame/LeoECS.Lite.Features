namespace Game.Ecs.AI.Tools
{
    using Components;
    using Leopotam.EcsLite;

    public static class AiSystemsTools
    {
        public static bool IsPlannerEnabledForEntity(EcsWorld world, int entity, int plannerId)
        {
            var pool = world.GetPool<AiAgentComponent>();
            ref var aiAgentComponent = ref pool.Get(entity);
            var availableActions = aiAgentComponent.AvailableActions;
            return availableActions.Length > plannerId && availableActions[plannerId];
        }
    }
}
