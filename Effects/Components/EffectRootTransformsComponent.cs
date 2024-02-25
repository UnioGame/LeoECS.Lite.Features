namespace Game.Ecs.Effects.Components
{
    using System;
    using Leopotam.EcsLite;
    using UnityEngine;

    /// <summary>
    /// target of effect root in the world
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct EffectRootTransformsComponent : IEcsAutoReset<EffectRootTransformsComponent>
    {
        public Transform[] Value;
        
        public void AutoReset(ref EffectRootTransformsComponent c)
        {
            c.Value ??= Array.Empty<Transform>();
            for (var i = 0; i < c.Value.Length; i++)
                c.Value[i] = default;
        }
    }
}