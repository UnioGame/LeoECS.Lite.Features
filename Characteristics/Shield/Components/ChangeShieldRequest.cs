namespace Game.Ecs.Characteristics.Shield.Components
{
    using Leopotam.EcsLite;

    public struct ChangeShieldRequest
    {
        public float Value;
        
        public EcsPackedEntity Source;
        public EcsPackedEntity Destination;
    }
}