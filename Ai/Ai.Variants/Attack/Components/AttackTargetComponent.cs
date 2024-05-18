namespace Ai.Ai.Variants.Attack.Components
{
    using System;
    using Game.Ecs.AI.Data;
    using Leopotam.EcsLite;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AttackTargetComponent : IEcsAutoReset<AttackTargetComponent>
    {
        public EcsPackedEntity Value;
        public int Priority;
        public void AutoReset(ref AttackTargetComponent c)
        {
            c.Value = default;
            c.Priority = AiConstants.PriorityNever;
        }
    }
}