namespace Game.Ecs.Ability.AbilityUtilityView.Highlights.Systems
{
    using Components;
    using Leopotam.EcsLite;
    using ViewControl.Components;

    public sealed class ProcessHideHighlightRequestSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<HideHighlightRequest>().End();

        }
        
        public void Run(IEcsSystems systems)
        {
            var requestPool = _world.GetPool<HideHighlightRequest>();
            var highlightStatePool = _world.GetPool<HighlightStateComponent>();
            var hideViewPool = _world.GetPool<HideViewRequest>();

            foreach (var entity in _filter)
            {
                ref var request = ref requestPool.Get(entity);
                if(!request.Source.Unpack(_world, out var sourceEntity) || !highlightStatePool.Has(sourceEntity))
                    continue;

                ref var state = ref highlightStatePool.Get(sourceEntity);
                if(!state.Highlights.TryGetValue(request.Destination, out var view))
                    continue;
                
                state.Highlights.Remove(request.Destination);
                
                if(state.Highlights.Count == 0)
                    highlightStatePool.Del(sourceEntity);

                var hideRequestEntity = _world.NewEntity();
                ref var hideViewRequest = ref hideViewPool.Add(hideRequestEntity);
                
                hideViewRequest.View = view;
                hideViewRequest.Destination = request.Destination;
            }
        }
    }
}