namespace Game.Code.Services.Ability.Editor
{
    using System;
    using AbilityLoadout.Data;
    using Animations;
    using Configuration.Runtime.Ability;
    using UnityEngine.Timeline;

    [Serializable]
    public struct AbilityEditorData
    {
        public AbilityItemAsset meta;
        public AbilityConfiguration configuration;
        public AnimationLink animationLink;
        public TimelineAsset animation;
    }
}