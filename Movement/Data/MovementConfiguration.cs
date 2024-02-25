namespace Game.Code.Configuration.Runtime.Entity.Movement
{
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.Serialization;

    [CreateAssetMenu(fileName = "Movement Configuration", menuName = "Game/Configurations/Entity/Movement Configuration", order = 0)]
    public sealed class MovementConfiguration : ScriptableObject
    {
        [FormerlySerializedAs("_speed")]
        [SerializeField]
        public float speed = 5.0f;
        [FormerlySerializedAs("_angularSpeed")] 
        [SerializeField]
        public float angularSpeed = 720.0f;
        [FormerlySerializedAs("_navMeshStep")] 
        [SerializeField]
        public float navMeshStep = 1.0f;
        [FormerlySerializedAs("_animationRunSpeed")] 
        [SerializeField]
        public float animationRunSpeed = 5.0f;
        [FormerlySerializedAs("_maxAnimationRunSpeed")] 
        [SerializeField]
        public float maxAnimationRunSpeed = 6.0f;

        [HideInInspector]
        [InlineProperty]
        [HideLabel]
        public MovementData movementData = new MovementData();
        
        public float Speed => speed;
        
        public float AngularSpeed => angularSpeed;

        public float NavMeshStep => navMeshStep;

        public float AnimationRunSpeed => animationRunSpeed;

        public float MaxAnimationRunSpeed => maxAnimationRunSpeed;
    }
}