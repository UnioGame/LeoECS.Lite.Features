namespace Game.Code.Timeline.Addressables
{
    using System;
    using Shared;
    using UnityEngine;

    [Serializable]
    public class AddressableLoaderMixerBehaviour
        : AnimationMixerBehaviour<GameObject, AddressableLoaderAnimationMixer, AddressableAssetLoaderAnimationBehaviour>
    { }
}