namespace Game.Code.Timeline.Addressables
{
    using System;
    using Shared;
    using UnityEngine;

    [Serializable]
    public abstract class AddressableAnimationBehaviour : AnimationBehaviour
    {
        
        public AddressableBehaviourState state = new AddressableBehaviourState();
        
        public abstract void Load(GameObject source,float inputWeight, float progress);
    }
}