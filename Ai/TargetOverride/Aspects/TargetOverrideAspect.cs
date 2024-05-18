namespace Game.Ecs.AI.TargetOverride.Aspects
{
    using System;
    using Components;
    using global::Ai.Ai.Variants.Prioritizer.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class TargetOverrideAspect : EcsAspect
    {
        public EcsPool<DistanceTargetOverrideComponent> DistanceOverrideEnd;
        public EcsPool<DistanceTargetOverrideLockComponent> DistanceOverrideLock;

        public EcsPool<TargetOverrideLockComponent> OverrideLock;
    }
}