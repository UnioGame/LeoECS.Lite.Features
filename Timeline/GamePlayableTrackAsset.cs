namespace Game.Code.Timeline
{
    using System;
    using Game.Code.Timeline.Shared;
    using UnityEngine;
    using UnityEngine.Timeline;

    [TrackColor(0.2098f, 0.4529f, 0.4392f)]
    [TrackBindingType(typeof(GameObject))]
    //[TrackClipType(typeof("PUT CLIP NAME HERE"))]
    //[DisplayName("Game/PUT TRACK NAME HERE")]
    public class GameAnimationTrack
        : AnimationTrack<GameObject, GameAnimationMixer, GameAnimationMixerBehaviour,
            GameAnimationPlayableData> { }
    
    [Serializable]
    public class GameAnimationMixerBehaviour : 
        AnimationMixerBehaviour<GameObject, GameAnimationMixer, GameAnimationPlayableData> { }
    
    [Serializable]
    public class GameAnimationClip : AnimationTimelineClip<GameAnimationPlayableData> { }
    
    [Serializable]
    public class GameAnimationPlayableData : AnimationBehaviour { }
    
    [Serializable]
    public class GameAnimationMixer : AnimationMixer<GameObject, GameAnimationPlayableData>
    {
        protected override void OnSetupFrame(GameObject binding)
        {
            
        }
        
        public override void Blend(GameAnimationPlayableData behaviour, float inputWeight, float progress)
        {
            
        }

        public override void ApplyFrame()
        {
            
        }
    }

}