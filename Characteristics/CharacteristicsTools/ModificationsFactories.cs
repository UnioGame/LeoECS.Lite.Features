namespace Game.Ecs.Characteristics
{
    using System;
    using AbilityPower;
    using Base.Modification;
    using Cooldown;
    using Duration;
    using Health;

    [Serializable]
    public class CooldownModificationFactory : DefaultModificationFactory<CooldownModificationHandler>{}
    
    [Serializable]
    public class DurationModificationFactory : DefaultModificationFactory<DurationModificationHandler>{}
    
    [Serializable]
    public class HealthModificationFactory : DefaultModificationFactory<HealthModificationHandler>{}
}