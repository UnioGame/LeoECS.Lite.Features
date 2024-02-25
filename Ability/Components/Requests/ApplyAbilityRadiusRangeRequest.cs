namespace Game.Ecs.Characteristics.Attack.Components
{
    using System;
    using Leopotam.EcsLite;

    [Serializable]
    public struct ApplyAbilityRadiusRangeRequest
    {
        public EcsPackedEntity Target;
        public float Value;
        public int AbilitySlot;
    }
}