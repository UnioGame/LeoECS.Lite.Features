namespace Game.Code.Animations
{
    using System;
    using System.Linq;
    using PlayableBindings;
    using Resolvers;
    using UniGame.AddressableTools.Runtime;
    using UniModules.UniGame.Core.Runtime.DataFlow.Extensions;
    using UnityEngine;
    using UnityEngine.Playables;
    using UnityEngine.Timeline;

    [Serializable]
    public class AnimationTrackResolver : OutputTrackResolver<AnimationTrack,AnimationTrackReference>
    {
        protected override void OnResolve(PlayableDirector director,AnimationTrack track,AnimationTrackReference reference)
        {
            var clips = track.GetClips();

            foreach (var clip in clips)
            {
                if(clip.asset is not AnimationPlayableAsset clipAsset) continue;

                var targetClip = reference.Clips.FirstOrDefault(x => x.Clip == clipAsset);
                if(targetClip == null) continue;
                
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    clipAsset.clip = targetClip.Animation.editorAsset;
                    continue;
                }
#endif
                var lifeTime = director.gameObject.GetAssetLifeTime();
                clipAsset.clip = targetClip.Animation.LoadAssetForCompletion(lifeTime);
            }
            
        }
    }
}