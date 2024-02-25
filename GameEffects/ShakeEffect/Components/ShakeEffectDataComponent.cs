namespace Game.Ecs.GameEffects.ShakeEffect.Components
{
    using System;
    using UnityEngine;

    /// <summary>
    /// shake effect data description
    /// <summary>Shakes a Transform's localPosition with the given values.</summary>
    /// <param name="duration">The duration of the tween</param>
    /// <param name="strength">The shake strength on each axis</param>
    /// <param name="vibrato">Indicates how much will the shake vibrate</param>
    /// <param name="randomness">Indicates how much the shake will be random (0 to 180 - values higher than 90 kind of suck, so beware).
    /// Setting it to 0 will shake along a single direction.</param>
    /// <param name="snapping">If TRUE the tween will smoothly snap all values to integers</param>
    /// <param name="fadeOut">If TRUE the shake will automatically fadeOut smoothly within the tween's duration, otherwise it will not</param>
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct ShakeEffectDataComponent
    {
        [Tooltip("The duration of the tween")]
        public float Duration;
        [Tooltip("The shake strength on each axis")]
        public Vector3 Strength;
        [Tooltip("Indicates how much will the shake vibrate")]
        public int Vibrato;
        [Range(0,180)]
        [Tooltip("Indicates how much the shake will be random (0 to 180 - values higher than 90 kind of suck, so beware. Setting it to 0 will shake along a single direction")]
        public float Random;
        [Tooltip("If TRUE the tween will smoothly snap all values to integers")]
        public bool Snapping;
        [Tooltip("If TRUE the shake will automatically fadeOut smoothly within the tween's duration, otherwise it will not")]
        public bool FadeOut;
    }
}