namespace Game.Code.Timeline.Shared
{
    using System;
    using UnityEngine;
    using UnityEngine.Playables;
    using UnityEngine.Timeline;

    [Serializable]
    public abstract class AnimationTimelineClip<TAnimationBehaviour> : PlayableAsset, ITimelineClipAsset
        where TAnimationBehaviour : AnimationBehaviour, new()
    {
        public TAnimationBehaviour template = new TAnimationBehaviour();

        public ClipCaps clipCaps => ClipCaps.Blending;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<TAnimationBehaviour>.Create(graph, template);
            
            OnInitializeBehaviour(playable.GetBehaviour());
            
            return playable;
        }

        protected virtual void OnInitializeBehaviour(TAnimationBehaviour behaviour)
        {
            
        }
    }
}