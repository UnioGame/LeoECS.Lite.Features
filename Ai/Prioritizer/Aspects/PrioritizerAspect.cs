namespace Ai.Ai.Variants.Prioritizer.Aspects
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
    public class PrioritizerAspect : EcsAspect
    {
        public EcsPool<PrioritizerComponent> Priority;
        public EcsPool<AgroComponent> Agro;

        public EcsPool<DefaultTargetComponent> Default;
        public EcsPool<ChaseTargetComponent> Chase;
    }
}