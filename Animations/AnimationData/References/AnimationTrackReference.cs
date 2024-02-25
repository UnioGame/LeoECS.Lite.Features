namespace Game.Code.Animations.PlayableBindings
{
    using System;
    using System.Collections.Generic;
    using Resolvers;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using Object = UnityEngine.Object;

    [Serializable]
    public class AnimationTrackReference : IPlayableReference
    {
        public Object Track;
        public List<AnimationClipData> Clips = new List<AnimationClipData>();
    }

    [Serializable]
    public class AnimationClipData
    {
        public Object Clip;
        public AssetReferenceT<AnimationClip> Animation;
    }
}