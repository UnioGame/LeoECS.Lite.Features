namespace Game.Ecs.GameAi.Move.Components
{
    using System;
    using AI.Data;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Shared.Abstract;
    using UnityEngine;

    [Serializable]
    public struct MovePlannerComponent : IApplyableComponent<MovePlannerComponent>
    {
        [InlineProperty]
        [HideLabel]
        [SerializeField]
        public AiPlannerData PlannerData;

        public void Apply(ref MovePlannerComponent component)
        {
            component.PlannerData = PlannerData;
        }
    }
}
