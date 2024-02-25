namespace Game.Ecs.Animations.Components.Requests
{
    using System;
    using Code.Animations;
    using Leopotam.EcsLite;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Playables;
    using UnityEngine.Serialization;

    /// <summary>
    /// try play animation by link
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct CreateAnimationLinkSelfRequest
    {
        public EcsPackedEntity Owner;
        [FormerlySerializedAs("PlayableDirectorEntity")] public EcsPackedEntity Target;
        public AnimationLink Data;
    }
}