namespace Game.Ecs.Ai.Targeting.Aspects
{
    using System;
    using Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    using Leopotam.EcsLite;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class TargetingAspect : EcsAspect
    {
        public EcsPool<SelectByCategoryComponent> SelectByCategory;
        public EcsPool<SelectByAttackEventComponent> SelectByAttackEvent;
        public EcsPool<AttackEventTargetComponent> AttackEventTarget;
    }
}