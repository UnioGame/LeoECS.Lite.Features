namespace Game.Ecs.GameAi.ActivateAbility
{
    using Leopotam.EcsLite;

    /// <summary>
    /// ai ability selection event
    /// </summary>
    public struct AbilityAiActionTargetComponent
    {
        public int AbilityCellId;
        public EcsPackedEntity Ability;
        public EcsPackedEntity AbilityTarget;
    }

}