namespace Game.Ecs.AI.Components
{
    using System;
    using UniGame.LeoEcs.Shared.Abstract;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AiAgroRangeComponent : IApplyableComponent<AiAgroRangeComponent>
    {
        public float RangeSqr;
        
        public void Apply(ref AiAgroRangeComponent rangeComponent)
        {
            rangeComponent.RangeSqr = RangeSqr;
        }
    }
}