namespace Game.Ecs.Map.Systems
{
    using Camera.Components;
    using Component;
    using JetBrains.Annotations;
    using Leopotam.EcsLite;
    using UnityEngine;

    /// <summary>
    /// Система инициализации пространства карты.
    /// </summary>
    [UsedImplicitly]
    public sealed class MapSpaceInitializeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private int _mapEntity;
        private EcsFilter _filter;
        private EcsWorld _world;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<CameraComponent>().End();
            _mapEntity = _world.NewEntity();

            var matrixPool = _world.GetPool<MapMatrixComponent>();
            matrixPool.Add(_mapEntity);
        }

        public void Run(IEcsSystems systems)
        {
            var cameraPool = _world.GetPool<CameraComponent>();
            var matrixPool = _world.GetPool<MapMatrixComponent>();
            ref var matrix = ref matrixPool.Get(_mapEntity);
            
            foreach (var entity in _filter)
            {
                ref var camera = ref cameraPool.Get(entity);
                if(!camera.IsMain)
                    continue;

                matrix.Value = GetMapRotationMatrix(Mathf.Deg2Rad * camera.Camera.transform.eulerAngles.y);
            }
        }

        private static Matrix4x4 GetMapRotationMatrix(float angleRad)
        {
            var matrix = Matrix4x4.identity;                // cos 0 sin 0
            matrix.m00 = matrix.m22 = Mathf.Cos(angleRad);  //  0  1  0  0
            matrix.m02 = Mathf.Sin(angleRad);               //-sin 0 cos 0
            matrix.m20 = -matrix.m02;                       //  0  0  0  1
            return matrix;
        }
    }
}