namespace Game.Code.Timeline.Addressables
{
    using System;
    using UnityEngine;

    [Serializable]
    public class AddressableBehaviourState
    {
        [Range(0f,1f)]
        public float loadOnProgress = 0.5f;
        public bool singleLoadPerLifeTime = true;
        public bool ownLifeTime = true;
        
        [Sirenix.OdinInspector.ReadOnly]
        public bool isLoaded = false;
        [Sirenix.OdinInspector.ReadOnly]
        public int counter;
    }
}