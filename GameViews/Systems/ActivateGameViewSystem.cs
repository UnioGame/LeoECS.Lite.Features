namespace Game.Ecs.Gameplay.LevelProgress.Systems
{
    using System;
    using Aspects;
    using Components;
    using GameResources.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// activate new view
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ActivateGameViewSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _activateFilter;
        
        private ParentGameViewAspect _parentViewAspect;
        private GameViewAspect _viewAspect;
        
        private EcsPool<GameResourceSpawnRequest> _gameResourcePool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _activateFilter = _world
                .Filter<ActivateGameViewRequest>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var requestEntity in _activateFilter)
            {
                ref var activateRequest = ref _parentViewAspect.Activate.Get(requestEntity);
 
                if(!activateRequest.Source.Unpack(_world,out var target)) continue;
                if(!_parentViewAspect.Parent.Has(target)) continue;
                
                ref var viewParentComponent = ref _parentViewAspect.Parent.Get(target);
                    
                var gameResourceEntity = _world.NewEntity();
                ref var gameResourceRequest = ref _gameResourcePool.Add(gameResourceEntity);

                var viewEntity = _world.NewEntity();
                ref var viewComponent = ref _viewAspect.View.Add(viewEntity);
                var viewPacked = _world.PackEntity(viewEntity);
                
                gameResourceRequest.Source = activateRequest.Source;
                gameResourceRequest.Owner = activateRequest.Source;
                gameResourceRequest.ParentEntity = activateRequest.Source;

                gameResourceRequest.Target = viewPacked;
                
                gameResourceRequest.ResourceId = activateRequest.View;
                gameResourceRequest.Parent = viewParentComponent.Parent;
                gameResourceRequest.LocationData.Rotation = viewParentComponent.Rotation;
                gameResourceRequest.LocationData.Position = viewParentComponent.Position;
                gameResourceRequest.LocationData.Scale = viewParentComponent.Scale;

                ref var activeViewComponent = ref _parentViewAspect.ActiveView.GetOrAddComponent(target);
                activeViewComponent.Value = viewPacked;
            }
        }
    }
}