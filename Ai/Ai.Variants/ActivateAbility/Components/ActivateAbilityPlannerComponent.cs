namespace Game.Code.Ai.ActivateAbility
{
    using System;
    using Ecs.AI.Service;
    using GameLayers.Category;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Shared.Abstract;
    using UnityEngine;

    [Serializable]
    public struct ActivateAbilityPlannerComponent : IApplyableComponent<ActivateAbilityPlannerComponent>
    {
        /// <summary>
        /// Action Planner Data
        /// </summary>
        [InlineProperty]
        [SerializeField]
        [HideLabel]
        public AiPlannerData PlannerData;

        public void Apply(ref ActivateAbilityPlannerComponent component)
        {
            component.PlannerData = PlannerData;
        }
    }

    [Serializable]
    [HorizontalGroup(nameof(CategoryPriority),LabelWidth = 40)]
    public struct CategoryPriority
    {
        public CategoryId Category;
        public float Value;
    }
}