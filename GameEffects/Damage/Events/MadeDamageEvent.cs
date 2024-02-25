namespace Game.Ecs.Gameplay.Damage.Components
{
    using Leopotam.EcsLite;

    public struct MadeDamageEvent : IEcsAutoReset<MadeDamageEvent>
    {
        public float Value;
        public bool IsCritical;
        
        public EcsPackedEntity Source;
        public EcsPackedEntity Destination;
        
        public void AutoReset(ref MadeDamageEvent c)
        {
            c.Value = 0.0f;
            c.IsCritical = false;
        }
    }
}