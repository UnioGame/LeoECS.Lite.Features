namespace MovementTweenAnimation.Components
{
    using System;
    using Game.Runtime.Tools.Tweens;

    /// <summary>
    /// Component that holds information for a movement tween animation component.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct MovementTweenAnimationComponent
    {
        public ScaleTween MoveTween;
    }
}