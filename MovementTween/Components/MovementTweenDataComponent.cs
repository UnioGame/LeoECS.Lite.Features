namespace Game.Ecs.Movement.Components
{
    using System;
    using Leopotam.EcsLite;
    using PrimeTween;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct MovementTweenDataComponent : IEcsAutoReset<MovementTweenDataComponent>
    {
        public Tween Tween;
        public int Index;
        public bool IsCompleted;
        
        public void AutoReset(ref MovementTweenDataComponent c)
        {
            c.Tween = default;
            c.Index = 0;
            c.IsCompleted = false;
        }
    }
}