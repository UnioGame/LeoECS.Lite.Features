namespace Game.Ecs.GameAi.MoveToTarget.Systems
{
    using Game.Ecs.AI.Components;
    using Game.Ecs.GameAi.MoveToTarget.Components;
    using Leopotam.EcsLite;

    public class ClearMoveToTargetsSystem : IEcsRunSystem,IEcsInitSystem
    {
        
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AiAgentComponent>()
                .Inc<MoveToGoalComponent>()
                .Inc<MoveToTargetPlannerComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var goalPool = _world.GetPool<MoveToGoalComponent>();

            foreach (var entity in _filter)
            {
                ref var goalComponent = ref goalPool.Get(entity);
                goalComponent.Goals.Clear();
            }
        }
    }
}