namespace Game.Ecs.Characteristics
{
    using System;
    using Attack;
    using Base.Modification;

    [Serializable]
    public class AttackModificationFactory : DefaultModificationFactory<AttackDamageModificationHandler>{}
}