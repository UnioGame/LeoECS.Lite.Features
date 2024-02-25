namespace Game.Ecs.Ability.AbilityUtilityView.Area.Systems
{
    using Components;
    using Leopotam.EcsLite;
    using SubFeatures.Area.Components;
    using UnityEngine;

    public sealed class DestroyAreaSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AreaInstanceComponent>()
                .Exc<AreaLocalPositionComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var areaInstancePool = _world.GetPool<AreaInstanceComponent>();

            foreach (var entity in _filter)
            {
                ref var areaInstance = ref areaInstancePool.Get(entity);
                
                if (areaInstance.Instance != null)
                    Object.Destroy(areaInstance.Instance);
                
                areaInstancePool.Del(entity);
            }
        }
    }
}