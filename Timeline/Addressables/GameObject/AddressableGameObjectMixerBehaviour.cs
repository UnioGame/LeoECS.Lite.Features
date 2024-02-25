namespace Game.Code.Timeline.Addressables
{
    using System;
    using Shared;
    using UnityEngine;

    [Serializable]
    public class AddressableGameObjectMixerBehaviour
        : AnimationMixerBehaviour<GameObject, AddressableGameObjectAnimationMixer, AddressableGameObjectAnimationBehaviour>
    { }
}