namespace Game.Ecs.Characteristics.Cooldown.Components
{
    using Leopotam.EcsLite;

    public struct CooldownComponent : IEcsAutoReset<CooldownComponent>
    {
        public float Value;
        
        public void AutoReset(ref CooldownComponent c)
        {
            c.Value = 0;
        }
    }
}