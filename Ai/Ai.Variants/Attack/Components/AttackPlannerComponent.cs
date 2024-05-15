namespace Ai.Ai.Variants.Attack.Components
{
    using System;
    using Game.Ecs.AI.Data;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Shared.Abstract;
    using UnityEngine;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AttackPlannerComponent : IApplyableComponent<AttackPlannerComponent>
    {
        [InlineProperty]
        [HideLabel]
        [SerializeField]
        public AiPlannerData PlannerData;

        public void Apply(ref AttackPlannerComponent component)
        {
            component.PlannerData = PlannerData;
        }
    }
}