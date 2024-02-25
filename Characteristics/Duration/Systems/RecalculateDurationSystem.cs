namespace Game.Ecs.Characteristics.Duration.Systems
{
    using Base.Modification;
    using Components;
    using Leopotam.EcsLite;

    public sealed class RecalculateDurationSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<RecalculateDurationRequest>()
                .Inc<BaseDurationComponent>()
                .Inc<DurationComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            var baseDurationPool = _world.GetPool<BaseDurationComponent>();
            var durationPool = _world.GetPool<DurationComponent>();

            foreach (var entity in _filter)
            {
                ref var baseDuration = ref baseDurationPool.Get(entity);
                ref var duration = ref durationPool.Get(entity);

                duration.Value = baseDuration.Modifications.Apply(baseDuration.Value);
            }
        }
    }
}