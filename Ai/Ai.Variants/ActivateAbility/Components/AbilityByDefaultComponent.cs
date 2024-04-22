namespace Game.Ecs.GameAi.ActivateAbility.Components
{
    using System;
    using Code.Ai.ActivateAbility;
    using UniGame.LeoEcs.Shared.Abstract;

    /// <summary>
    /// Stores the setting for the AI planner, allows you to use abilities when it possible without special conditions
    /// </summary>
    [Serializable]
    public struct AbilityByDefaultComponent : IApplyableComponent<AbilityByDefaultComponent>
    {
        public float Priority;
        public AbilityFilter[] FilterData;

        public void Apply(ref AbilityByDefaultComponent component)
        {
            component.Priority = Priority;
            component.FilterData = FilterData;
        }
    }
}
