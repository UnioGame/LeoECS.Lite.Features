namespace Game.Ecs.ViewControl.Components
{
    using Leopotam.EcsLite;
    using UnityEngine;

    public struct HideViewRequest
    {
        public EcsPackedEntity Destination;

        public GameObject View;
    }
}