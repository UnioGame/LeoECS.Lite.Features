namespace Game.Ecs.GameAi.ActivateAbility.Components
{
    using System;
    using Code.Ai.ActivateAbility;
    using UniGame.LeoEcs.Shared.Abstract;
    using UnityEngine;

    /// <summary>
    /// Stores the setting for the AI planner, allows you to use abilities depending on the distance to the target
    /// </summary>
    [Serializable]
    public struct AbilityByRangeComponent : IApplyableComponent<AbilityByRangeComponent>
    {
        public float Priority;
        public Vector3 Center;
        public float Radius;
        public float MinDistance;
        public AbilityFilter[] FilterData;

        public void Apply(ref AbilityByRangeComponent component)
        {
            component.Center = Center;
            component.Radius = Radius;
            component.Priority = Priority;
            component.MinDistance = MinDistance;
            component.FilterData = FilterData;
        }
    }
}
