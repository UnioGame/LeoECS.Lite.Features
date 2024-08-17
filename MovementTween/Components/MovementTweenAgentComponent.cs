namespace Game.Ecs.Movement.Components
{
    using System;
    using Leopotam.EcsLite;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct MovementTweenAgentComponent : IEcsAutoReset<MovementTweenAgentComponent>
    {
        public int TrackId;
        public EcsPackedEntity Track;
        
        public void AutoReset(ref MovementTweenAgentComponent c)
        {
            c.TrackId = -1;
            c.Track = default;
        }
    }
}