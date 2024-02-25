namespace Game.Ecs.Gameplay.Damage.Components.Request
{
    using Leopotam.EcsLite;

    public struct ApplyDamageRequest : IEcsAutoReset<ApplyDamageRequest>
    {
        public float Value;
        public bool IsCritical;

        public EcsPackedEntity Source;
        public EcsPackedEntity Destination;
        public EcsPackedEntity Effector;
        
        public void AutoReset(ref ApplyDamageRequest c)
        {
            c.Value = 0.0f;
            c.IsCritical = false;
        }
    }
}