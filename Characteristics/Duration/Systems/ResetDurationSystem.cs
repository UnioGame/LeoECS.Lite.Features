namespace Game.Ecs.Characteristics.Duration.Systems
{
    using Base.Components.Events;
    using Components;
    using Leopotam.EcsLite;

    public sealed class ResetDurationSystem : IEcsRunSystem,IEcsInitSystem
    {
        
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<BaseDurationComponent>()
                .Inc<ResetCharacteristicsEvent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var baseDurationPool = _world.GetPool<BaseDurationComponent>();
            var requestPool = _world.GetPool<RecalculateDurationRequest>();

            foreach (var entity in _filter)
            {
                ref var baseDuration = ref baseDurationPool.Get(entity);
                baseDuration.Modifications.Clear();
                
                if (!requestPool.Has(entity))
                    requestPool.Add(entity);
            }
        }
    }
}