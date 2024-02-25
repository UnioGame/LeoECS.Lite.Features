namespace Game.Ecs.Movement.Components
{
    using UnityEngine;

    /// <summary>
    /// Целевая позиция для перемещения.
    /// Компонент является событием и удаляется в конце цикла.
    /// </summary>
    public struct MovementPointRequest
    {
        public Vector3 DestinationPosition;
    }
}