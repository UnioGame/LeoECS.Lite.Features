namespace Game.Code.Animations.ReferenceCollectors
{
    using System;
    using Abstract;
    using PlayableBindings;
    using UniGame.Shared.Runtime.Timeline;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Playables;
    using UnityEngine.Timeline;

#if UNITY_EDITOR
    using UniModules.UniGame.AddressableExtensions.Editor;
    using UniModules.Editor;
#endif
    
    [Serializable]
    public class AnimationTrackCollector :
        IPlayableReferenceCollector,
        IPlayableReferenceCleaner
    {
        public void CollectReferences(PlayableBindingData map, 
            PlayableAsset animation,  
            PlayableDirector playableDirector)
        {
            foreach (var animationOutput in animation.outputs)
            {
                var track = animationOutput.sourceObject;
                if(track is not AnimationTrack animationTrack) continue;
                
                var binding = map.bindings;
                
                var reference = new AnimationTrackReference();
                reference.Track = animationTrack;
                
                var clips = animationTrack.GetClips();

                foreach (var clip in clips)
                {
                    if(clip.asset is not AnimationPlayableAsset clipAsset) continue;

                    var animationClip = clipAsset.clip;
                    if(animationClip == null) continue;

#if UNITY_EDITOR
                    animationClip.AddToDefaultAddressableGroupIfNone();
                    animationClip.MarkDirty();
                    
                    var clipReference = new AssetReferenceT<AnimationClip>(animationClip.GetGUID());

                    var animationClipData = new AnimationClipData()
                    {
                        Animation = clipReference,
                        Clip = clipAsset
                    };

                    reference.Clips.Add(animationClipData);
#endif
                }
                
                binding.Add(reference);
            }
        }

        public void CleanReferences(PlayableDirector director,PlayableAsset animation)
        {
            foreach (var clip in animation.GetClips<AnimationPlayableAsset,AnimationTrack>())
            {
                clip.clip = null;
            }
        }
    }
}