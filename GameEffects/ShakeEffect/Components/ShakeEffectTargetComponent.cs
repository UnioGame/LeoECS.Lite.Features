namespace Game.Ecs.GameEffects.ShakeEffect.Components
{
    using System;
    using UnityEngine;

    /// <summary>
    /// set target for shake effect
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct ShakeEffectTargetComponent
    {
        public Transform Value;
    }
}