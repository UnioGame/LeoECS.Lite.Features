namespace Game.Code.Timeline.Activation
{
    using System;
    using UnityEngine;
    using UnityEngine.Playables;
    using UnityEngine.Timeline;

    [Serializable]
    public sealed class SetActiveClip : PlayableAsset, ITimelineClipAsset
    {
        [SerializeField]
        private SetActiveBehaviour _behaviour;
        [SerializeField]
        private ClipCaps _clipCaps;
        
        public ClipCaps clipCaps => _clipCaps;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            return ScriptPlayable<SetActiveBehaviour>.Create(graph, _behaviour);
        }
    }
}