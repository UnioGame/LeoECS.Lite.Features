namespace Game.Ecs.Map.Component
{
    using UnityEngine;

    /// <summary>
    /// Компонент хранящий матрицу пространства карты.
    /// Матрица зависит от главной камеры.
    /// </summary>
    public struct MapMatrixComponent
    {
        public Matrix4x4 Value;
    }
}