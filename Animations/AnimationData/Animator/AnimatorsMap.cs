namespace Animations.Animator.Data
{
    using System;
    using System.Collections.Generic;

    public struct AnimatorsMap
    {
        public Dictionary<AnimationClipId, StateAndClipData> data;
    }

    [Serializable]
    public struct StateAndClipData
    {
        public int stateNameHash;
        public string clipName;
    }
}