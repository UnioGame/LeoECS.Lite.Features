namespace Game.Ecs.ViewControl.Components
{
    using Leopotam.EcsLite;
    using UnityEngine;

    public struct ShowViewRequest
    {
        public EcsPackedEntity Destination;

        public GameObject View;
        
        public Transform Root;
        public Vector3 Size;
    }
}