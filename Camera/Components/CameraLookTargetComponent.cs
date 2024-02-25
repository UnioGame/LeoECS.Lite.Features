namespace Game.Ecs.Camera.Components
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Компонент цели следования камеры.
    /// </summary>
    [Serializable]
    public struct CameraLookTargetComponent
    {
        public float Speed;
        public Vector3 Offset;
    }
}