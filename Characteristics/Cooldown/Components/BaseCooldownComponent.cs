namespace Game.Ecs.Characteristics.Cooldown.Components
{
    using System.Collections.Generic;
    using Base.Modification;
    using Leopotam.EcsLite;

    public struct BaseCooldownComponent : IEcsAutoReset<BaseCooldownComponent>
    {
        public float Value;
        
        public List<Modification> Modifications;
        
        public void AutoReset(ref BaseCooldownComponent c)
        {
            c.Modifications ??= new List<Modification>();
        }
    }
}