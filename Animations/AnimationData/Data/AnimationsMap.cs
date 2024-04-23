namespace Animations.AnimationData.Data
{
    using System;
    using System.Collections.Generic;
    using Sirenix.OdinInspector;

    [Serializable]
    public struct AnimationsMap
    {
        [TableList]
        public List<AnimationKeyValuePair> animations;
    }
}