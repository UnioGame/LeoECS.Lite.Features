namespace Game.Ecs.Camera.Components
{
    using UnityEngine;

    /// <summary>
    /// Компонент с ссылкой на камеру.
    /// </summary>
    public struct CameraComponent
    {
        /// <summary>
        /// Ссылка на игровую камеру.
        /// </summary>
        public Camera Camera;
        /// <summary>
        /// Камера является главной?
        /// </summary>
        public bool IsMain;
    }
}