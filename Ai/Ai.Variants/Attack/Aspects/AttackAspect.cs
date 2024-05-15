namespace Ai.Ai.Variants.Attack.Aspects
{
    using System;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class AttackAspect : EcsAspect
    {
        public EcsPool<AttackPlannerComponent> Planner;
        public EcsPool<AttackActionComponent> Action;
        public EcsPool<AttackTargetComponent> Target;

        public EcsPool<AttackChaseTargetComponent> Chase;
    }
}