namespace Game.Ecs.Animations.Components
{
    using System;
    using Leopotam.EcsLite;
    using UnityEngine.Playables;

    /// <summary>
    /// playable animation asset
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AnimationPlayableComponent: IEcsAutoReset<AnimationPlayableComponent>
    {
        public PlayableAsset Value;
        
        public void AutoReset(ref AnimationPlayableComponent c)
        {
            c.Value = null;
        }
    }
}