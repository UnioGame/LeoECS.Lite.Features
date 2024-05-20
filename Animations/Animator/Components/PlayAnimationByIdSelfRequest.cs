namespace Animations.Animatror.Components
{
    using System;
    using Animator.Data;

    /// <summary>
    /// Play Animation by id self request
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct PlayAnimationByIdSelfRequest
    {
        public AnimationClipId id;
    }
}