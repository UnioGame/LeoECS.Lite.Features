namespace Game.Code.Animations.PlayableBindings
{
    using System;
    using System.Collections.Generic;
    using Resolvers;
    using UnityEngine.AddressableAssets;
    using Object = UnityEngine.Object;

    [Serializable]
    public class ControlTrackReference : IPlayableReference
    {
        public Object Track;
        public List<ControlTrackClip> Clips = new List<ControlTrackClip>();
    }

    [Serializable]
    public class ControlTrackClip
    {
        public Object Clip;
        public AssetReferenceGameObject Prefab;
    }
}