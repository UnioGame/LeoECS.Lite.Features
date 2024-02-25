namespace Game.Ecs.Ability.AbilityUtilityView.Area.Systems
{
    using Components;
    using Leopotam.EcsLite;
    using SubFeatures.Area.Components;
    using UnityEngine;

    public sealed class ShowAreaSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AreaLocalPositionComponent>()
                .Inc<AreaRadiusComponent>()
                .Inc<AreaRadiusViewComponent>()
                .Exc<AreaInstanceComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var areaViewPool = _world.GetPool<AreaRadiusViewComponent>();
            var areaRadiusPool = _world.GetPool<AreaRadiusComponent>();
            var areaInstancePool = _world.GetPool<AreaInstanceComponent>();

            foreach (var entity in _filter)
            {
                ref var areaRadius = ref areaRadiusPool.Get(entity);
                ref var areaView = ref areaViewPool.Get(entity);

                ref var areaInstance = ref areaInstancePool.Add(entity);

                var size = areaRadius.Value * 2.0f;
                areaInstance.Instance = Object.Instantiate(areaView.View);
                areaInstance.Instance.transform.localScale = new Vector3(size, size, size);
            }
        }
    }
}