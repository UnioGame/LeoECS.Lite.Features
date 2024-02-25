namespace Game.Ecs.Core.Components
{
    using System;
    using UnityEngine.Playables;

    [Serializable]
    public struct AnimationWrapModeComponent
    {
        public DirectorWrapMode Value;
    }
}