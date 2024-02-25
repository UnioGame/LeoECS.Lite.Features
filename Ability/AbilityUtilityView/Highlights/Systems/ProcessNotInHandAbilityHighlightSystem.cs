namespace Game.Ecs.Ability.AbilityUtilityView.Highlights.Systems
{
    using Common.Components;
    using Components;
    using Leopotam.EcsLite;

    public sealed class ProcessNotInHandAbilityHighlightSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<HighlightStateComponent>()
                .Exc<AbilityInHandComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var highlightStatePool = _world.GetPool<HighlightStateComponent>();
            var hideHighlightPool = _world.GetPool<HideHighlightRequest>();

            foreach (var entity in _filter)
            {
                ref var highlightedState = ref highlightStatePool.Get(entity);
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