namespace Game.Ecs.Movement.Components
{
    using Unity.Mathematics;

    /// <summary>
    /// Целевая позиция для перемещения.
    /// Компонент является событием и удаляется в конце цикла.
    /// </summary>
    public struct MovementPointRequest
    {
        public float3 DestinationPosition;
    }
}