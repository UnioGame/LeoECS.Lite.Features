namespace Game.Ecs.Ability.AbilityUtilityView.Highlights.Systems
{
    using Components;
    using Core.Components;
    using Leopotam.EcsLite;

    public sealed class ProcessHighlightWhenDeadSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<OwnerDestroyedEvent>()
                .Inc<HighlightStateComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var highlightedStatePool = _world.GetPool<HighlightStateComponent>();
            var hideHighlightPool = _world.GetPool<HideHighlightRequest>();

            foreach (var entity in _filter)
            {
                ref var highlightedState = ref highlightedStatePool.Get(entity);
                foreach (var packedEntity in highlightedState.Highlights)
                {
                    var hideRequestEntity = _world.NewEntity();
                    ref var hideRequest = ref hideHighlightPool.Add(hideRequestEntity);
                    
                    hideRequest.Source = _world.PackEntity(entity);
                    hideRequest.Destination = packedEntity.Key;
                }
            }
        }
    }
}