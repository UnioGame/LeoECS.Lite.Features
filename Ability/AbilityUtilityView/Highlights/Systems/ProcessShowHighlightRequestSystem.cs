namespace Game.Ecs.Ability.AbilityUtilityView.Highlights.Systems
{
    using Components;
    using Core.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    using ViewControl.Components;

    public sealed class ProcessShowHighlightRequestSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<ShowHighlightRequest>().End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var requestPool = _world.GetPool<ShowHighlightRequest>();
            var highlightPool = _world.GetPool<HighlightComponent>();
            var avatarPool = _world.GetPool<EntityAvatarComponent>();
            
            var highlightStatePool = _world.GetPool<HighlightStateComponent>();
            var showViewPool = _world.GetPool<ShowViewRequest>();

            foreach (var entity in _filter)
            {
                ref var request = ref requestPool.Get(entity);
                if(!request.Source.Unpack(_world, out var sourceEntity))
                    continue;

                ref var state = ref highlightStatePool.GetOrAddComponent(sourceEntity);
                if(state.Highlights.ContainsKey(request.Destination))
                    continue;

                if(!request.Destination.Unpack(_world, out var destinationEntity))
                    continue;
                
                if(!highlightPool.Has(destinationEntity) || !avatarPool.Has(destinationEntity))
                    continue;

                ref var highlight = ref highlightPool.Get(destinationEntity);
                ref var avatar = ref avatarPool.Get(destinationEntity);

                var showRequestEntity = _world.NewEntity();
                ref var showViewRequest = ref showViewPool.Add(showRequestEntity);
                
                showViewRequest.Root = avatar.Feet;
                showViewRequest.View = highlight.Highlight;
                var size = avatar.Bounds.Radius * 2.0f;
                showViewRequest.Size = new Vector3(size, size, size);
                
                showViewRequest.Destination = request.Destination;
                
                state.Highlights.Add(request.Destination, highlight.Highlight);
            }
        }
    }
}