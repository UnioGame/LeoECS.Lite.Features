namespace Game.Ecs.Ability.Common.Components
{
    using System;
    using Leopotam.EcsLite;

    /// <summary>
    /// Текущее умение в руке.
    /// </summary>
    [Serializable]
    public struct AbilityInHandLinkComponent
    {
        public EcsPackedEntity AbilityEntity;
    }
}