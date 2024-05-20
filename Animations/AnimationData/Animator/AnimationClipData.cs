namespace Animations.Animator.Data
{
    using System;

    [Serializable]
    public struct AnimationClipData
    {
        public string stateName;
        public AnimationClipId clipId;
        public string clipName;
    }
}