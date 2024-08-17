namespace Game.Ecs.Movement.Components
{
    using System;
    using Converters;
    using Leopotam.EcsLite;
    using Unity.Collections;


#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct MovementTweenTrackComponent : IEcsAutoReset<MovementTweenTrackComponent>
    {
        public int Id;
        public NativeArray<MovementTrackPoint> Points;
        
        public void AutoReset(ref MovementTweenTrackComponent c)
        {
            c.Id = -1;
            if (c.Points.IsCreated)
                c.Points.Dispose();
        }
    }
}