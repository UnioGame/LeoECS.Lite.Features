namespace Game.Code.Configuration.Runtime.Entity.Movement
{
    using System;
    using UnityEngine;

    [Serializable]
    public class MovementData
    {
        [SerializeField]
        public float speed = 5.0f;
        [SerializeField]
        public float angularSpeed = 720.0f;
        [SerializeField]
        public float navMeshStep = 1.0f;
        [SerializeField]
        public float animationRunSpeed = 5.0f;
        [SerializeField]
        public float maxAnimationRunSpeed = 6.0f;
        [SerializeField]
        public bool instantRotation = false;
    }
}