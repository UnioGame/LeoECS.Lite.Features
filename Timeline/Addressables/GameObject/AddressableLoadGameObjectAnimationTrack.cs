namespace Game.Code.Timeline.Addressables
{
    using System.ComponentModel;
    using Shared;
    using UnityEngine;
    using UnityEngine.Timeline;

    [TrackColor(0.2098f, 0.4529f, 0.4392f)]
    [TrackBindingType(typeof(GameObject))]
    [TrackClipType(typeof(AddressableGameObjectAnimationClip))]
    [DisplayName("Addressable/GameObject")]
    public class AddressableLoadGameObjectAnimationTrack
        : AnimationTrack<GameObject, AddressableGameObjectAnimationMixer, AddressableGameObjectMixerBehaviour,
            AddressableGameObjectAnimationBehaviour> { }
}