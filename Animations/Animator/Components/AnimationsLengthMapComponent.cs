namespace Animations.Animatror.Components
{
    using System;
    using System.Collections.Generic;
    using Animator.Data;
    using Leopotam.EcsLite;

    /// <summary>
    /// map State Name - Clip Length
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AnimationsLengthMapComponent : IEcsAutoReset<AnimationsLengthMapComponent>
    {
        public Dictionary<AnimationClipId, float> value;
        public void AutoReset(ref AnimationsLengthMapComponent c)
        {
            c.value ??= new();
            c.value.Clear();
        }
    }
}