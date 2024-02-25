namespace Game.Ecs.Ability.AbilityUtilityView.Area.Systems
{
    using Components;
    using Core.Components;
    using Core.Death.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    public sealed class DestroyAreaByOwnerSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<AreaInstanceComponent> _areaInstancePool;
        private EcsPool<KillRequest> _killRequest;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<OwnerDestroyedEvent>()
                .Inc<AreaInstanceComponent>()
                .End();
            
            _areaInstancePool = _world.GetPool<AreaInstanceComponent>();
            _killRequest = _world.GetPool<KillRequest>();
        }
        
        public void Run(IEcsSystems systems)
        {
            
            foreach (var entity in _filter)
            {
                ref var areaInstance = ref _areaInstancePool.Get(entity);
                _areaInstancePool.Del(entity);
                
                if (areaInstance.Instance == null)
                    continue;
                
                Object.Destroy(areaInstance.Instance);
            }
        }
    }
}