namespace Game.Ecs.Input.Systems
{
    using Components.Direction;
    using Leopotam.EcsLite;
    using Map;
    using Map.Component;
    using UnityEngine;

    /// <summary>
    /// Система отвечающая за конвертацию данных джойстика с пользовательного ввода в пространство карты.
    /// Пространство карты зависимо от главной камеры. Матрица пространства карты находится в компоненте <see cref="MapMatrixComponent"/>.
    /// </summary>
    public sealed class DirectionRawMapConvertSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<DirectionRawInputEvent>().End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var rawPool = _world.GetPool<DirectionRawInputEvent>();
            var pool = _world.GetPool<DirectionInputEvent>();
            
            var matrix = MapHelper.GetMatrix(_world);

            foreach (var entity in _filter)
            {
                ref var rawData = ref rawPool.Get(entity);
                ref var data = ref pool.Add(entity);

                var rawDirection = rawData.Value;
                var identityDirection = rawDirection.x * Vector3.right + rawDirection.y * Vector3.forward;
                var mapDirection = matrix.MultiplyVector(identityDirection);

                data.Value = mapDirection;
            }
        }
    }
}