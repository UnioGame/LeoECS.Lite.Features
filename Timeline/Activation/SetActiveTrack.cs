namespace Game.Code.Timeline.Activation
{
    using System.ComponentModel;
    using UnityEngine;
    using UnityEngine.Playables;
    using UnityEngine.Timeline;

    [TrackClipType(typeof(SetActiveClip))]
    [DisplayName("GameObject/SetActive Track")]
    [TrackBindingType(typeof(GameObject))]
    public sealed class SetActiveTrack : TrackAsset
    {
        [SerializeField]
        private bool _endValue;
        
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<SetActiveMixerBehaviour>.Create(graph, new SetActiveMixerBehaviour(_endValue), inputCount);
        }
    }
}