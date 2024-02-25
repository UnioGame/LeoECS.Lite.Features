namespace Game.Code.Animations.EffectMilestones.Timeline
{
    using UnityEngine;
    using UnityEngine.Playables;
    using UnityEngine.Timeline;

    public sealed class EffectMilestoneClip : PlayableAsset, ITimelineClipAsset
    {
        public ClipCaps clipCaps => ClipCaps.None;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<EffectMilestoneBehaviour>.Create(graph, new EffectMilestoneBehaviour());
            return playable;
        }
    }
}