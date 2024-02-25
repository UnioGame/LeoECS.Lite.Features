namespace Game.Ecs.Gameplay.LevelProgress.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// disable current active view
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class RemoveActiveViewSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private ParentGameViewAspect _parentViewAspect;
        private EcsFilter _disableViewFilter;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _disableViewFilter = _world
                .Filter<DisableActiveGameViewRequest>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _disableViewFilter)
            {
                ref var disableRequest = ref _parentViewAspect.Disable.Get(entity);
                if(!disableRequest.Value.Unpack(_world,out var targetEntity))
                    continue;
                
                _parentViewAspect.ActiveView.TryRemove(targetEntity);
            }
        }
    }
}