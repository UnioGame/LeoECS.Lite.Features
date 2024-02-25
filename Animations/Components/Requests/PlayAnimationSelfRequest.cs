namespace Game.Ecs.Animations.Components.Requests
{
    using System;
    using Leopotam.EcsLite;
    using UnityEngine.Playables;

    /// <summary>
    /// play exists animation
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct PlayAnimationSelfRequest : IEcsAutoReset<PlayAnimationSelfRequest>
    {
        //OPTIONAL - animation start time
        public float StartTime;
        //OPTIONAL - animation duration
        public float Duration;
        //OPTIONAL - animation speed
        public float Speed;
        
        public void AutoReset(ref PlayAnimationSelfRequest c)
        {
            c.Duration = 0;
            c.StartTime = 0;
            c.Speed = 0;
        }
    }
}