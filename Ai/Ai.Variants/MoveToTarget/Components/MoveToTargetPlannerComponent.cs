namespace Game.Ecs.GameAi.MoveToTarget.Components
{
    using System;
    using Game.Ecs.AI.Data;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Shared.Abstract;
    using UnityEngine;

    [Serializable]
    public struct MoveToTargetPlannerComponent : IApplyableComponent<MoveToTargetPlannerComponent>
    {
        [InlineProperty]
        [HideLabel]
        [SerializeField]
        public AiPlannerData PlannerData;

        public void Apply(ref MoveToTargetPlannerComponent component)
        {
            component.PlannerData = PlannerData;
        }
    }
}
