namespace Game.Ecs.Ability.AbilityUtilityView.Radius.Component
{
    using Leopotam.EcsLite;
    using UnityEngine;

    public struct ShowRadiusRequest
    {
        public EcsPackedEntity Source;
        public EcsPackedEntity Destination;

        public Transform Root;
        public GameObject Radius;
        
        public Vector3 Size;
    }
}