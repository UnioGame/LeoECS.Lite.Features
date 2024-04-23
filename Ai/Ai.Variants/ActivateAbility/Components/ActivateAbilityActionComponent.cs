namespace Game.Code.Ai.ActivateAbility
{
    using System;
    using Leopotam.EcsLite;

    [Serializable]
    public struct ActivateAbilityActionComponent
    {
        public int AbilityCellId;
        public EcsPackedEntity Target;
        public EcsPackedEntity Ability;
    }
}
