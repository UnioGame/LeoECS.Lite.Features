namespace Game.Ecs.Characteristics.Duration.Components
{
    using System.Collections.Generic;
    using Base.Modification;
    using Leopotam.EcsLite;

    public struct BaseDurationComponent : IEcsAutoReset<BaseDurationComponent>
    {
        public float Value;
        
        public List<Modification> Modifications;
        
        public void AutoReset(ref BaseDurationComponent c)
        {
            c.Modifications ??= new List<Modification>();
        }
    }
}