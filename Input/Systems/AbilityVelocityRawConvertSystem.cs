namespace Game.Ecs.Input.Systems
{
    using Components.Ability;
    using Leopotam.EcsLite;
    using Map;
    using UnityEngine;

    public sealed class AbilityVelocityRawConvertSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world= systems.GetWorld();
            _filter = _world.Filter<AbilityVelocityRawEvent>().End();
        }
        
        public void Run(IEcsSystems systems)
        {

            var rawPool = _world.GetPool<AbilityVelocityRawEvent>();
            var pool = _world.GetPool<AbilityCellVelocityEvent>();
            
            var matrix = MapHelper.GetMatrix(_world);

            foreach (var entity in _filter)
            {
                ref var rawData = ref rawPool.Get(entity);
                ref var data = ref pool.Add(entity);

                var rawDirection = rawData.Value;
                var identityDirection = rawDirection.x * Vector3.right + rawDirection.y * Vector3.forward;
                var mapDirection = matrix.MultiplyVector(identityDirection);

                data.AbilityCellId = rawData.AbilityCellId;
                data.Value = mapDirection;
            }
        }
    }
}