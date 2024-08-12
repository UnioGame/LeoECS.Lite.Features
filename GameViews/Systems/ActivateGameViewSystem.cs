namespace Game.Ecs.Gameplay.LevelProgress.Systems
{
    using System;
    using Aspects;
    using Components;
    using GameResources.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.Di;
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
    public class ActivateGameViewSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        
        private ParentGameViewAspect _parentViewAspect;
        private GameViewAspect _viewAspect;
        
        private EcsPool<GameResourceSpawnRequest> _gameResourcePool;
        private EcsFilterInject<Inc<ActivateGameViewRequest>> _activateFilter;

        public void Run(IEcsSystems systems)
        {
            foreach (var requestEntity in _activateFilter.Value)
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

                gameResourceRequest.Entity = viewPacked;
                
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