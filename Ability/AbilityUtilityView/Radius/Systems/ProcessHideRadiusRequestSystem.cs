namespace Game.Ecs.Ability.AbilityUtilityView.Radius.Systems
{
    using Component;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
    using ViewControl.Components;

    public sealed class ProcessHideRadiusRequestSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<HideRadiusRequest>().End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var requestPool = _world.GetPool<HideRadiusRequest>();
            var radiusStatePool = _world.GetPool<RadiusViewStateComponent>();
            var hideViewPool = _world.GetPool<HideViewRequest>();

            foreach (var entity in _filter)
            {
                ref var request = ref requestPool.Get(entity);
                if(!request.Source.Unpack(_world, out var sourceEntity))
                    continue;
                
                ref var state = ref radiusStatePool.GetOrAddComponent(sourceEntity);
                if(!state.RadiusViews.TryGetValue(request.Destination, out var radiusView))
                    continue;
                
                state.RadiusViews.Remove(request.Destination);
                
                if(state.RadiusViews.Count == 0)
                    radiusStatePool.Del(sourceEntity);
                
                var hideRequestEntity = _world.NewEntity();
                ref var hideViewRequest = ref hideViewPool.Add(hideRequestEntity);
                
                hideViewRequest.View = radiusView;
                hideViewRequest.Destination = request.Destination;
            }
        }
    }
}