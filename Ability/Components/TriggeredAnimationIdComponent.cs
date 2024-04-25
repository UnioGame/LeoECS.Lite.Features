namespace Ability.Components
{
    using System;

    /// <summary>
    /// Какую анимацию мы должны будем триггерить у аниматора
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct TriggeredAnimationIdComponent
    {
        public string animationId;
    }
}