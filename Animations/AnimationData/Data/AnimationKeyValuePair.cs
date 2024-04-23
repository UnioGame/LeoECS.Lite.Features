namespace Animations.AnimationData.Data
{
    using System;
    using Game.Code.Animations;

    [Serializable]
    public struct AnimationKeyValuePair
    {
        public string key;
        public AnimationClipId value;
    }
}