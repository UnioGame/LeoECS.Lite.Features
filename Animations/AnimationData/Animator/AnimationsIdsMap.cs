namespace Animations.Animator.Data
{
    using System;
    using System.Collections.Generic;
    using Sirenix.OdinInspector;

    [Serializable]
    public struct AnimationsIdsMap
    {
        [TableList]
        public List<AnimationClipData> animations;
    }
}