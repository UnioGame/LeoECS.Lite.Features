namespace Ai.Ai.Variants.Prioritizer.Aspects
{
    using System;
    using Components;
    using Game.Ecs.AI.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class PrioritizerAspect : EcsAspect
    {
        public EcsPool<CategoryPrioritizerComponent> Priority;
        public EcsPool<PrioritizedTargetComponent> Target;
    }
}