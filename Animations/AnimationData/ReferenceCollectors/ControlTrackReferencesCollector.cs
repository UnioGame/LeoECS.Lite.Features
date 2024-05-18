namespace Game.Code.Animations.PlayableBindings
{
    using System;
    using Abstract;
    using Modules.UnioModules.UniGame.CoreModules.UniGame.Core.Runtime.Extension;
    using UnityEngine.Playables;

#if UNITY_EDITOR
    using UniModules.Editor;
    using UnityEditor;
    using UniModules.UniGame.AddressableExtensions.Editor;
#endif

    [Serializable]
    public class ControlTrackReferencesCollector : 
        IPlayableReferenceCollector,
        IPlayableReferenceCleaner
    {
        public void CollectReferences(PlayableBindingData map, PlayableAsset animation, PlayableDirector director)
        {
            foreach (var animationOutput in animation.outputs)
            {
                /*var source = animationOutput.sourceObject;
                if (source is not ControlTrack trackAsset) continue;
                
                var clips = trackAsset.GetClips();
                var bindings = map.bindings;
                
                var controlTrackReference = new ControlTrackReference()
                {
                    Track = trackAsset
                };
            
                foreach (var clip in clips)
                {
                    if(clip.asset is not ControlPlayableAsset clipAsset) continue;

                    var exposedValue = clipAsset.sourceGameObject;
                    var resolvedValue = exposedValue.Resolve(director);
                    if(resolvedValue == null) continue;
                    
                    var name = resolvedValue.name;
                    var gameObject = director.gameObject;
                    var childObject = gameObject.FindChildGameObject(name);

                    var reference = new GameObjectExposedReference()
                    {
                        Id = exposedValue.exposedName.GetHashCode(),
                        Child = childObject != null,
                        Name = name,
                        Guid = string.Empty
                    };
                    
#if UNITY_EDITOR
                    clipAsset.AddToDefaultAddressableGroupIfNone();
                    clipAsset.MarkDirty();
                    
                    var controlClip = new ControlTrackClip()
                    {
                        Prefab = new AssetReferenceGameObject(clipAsset.prefabGameObject.GetGUID()),
                        Clip = clip.asset,
                    };
                    controlTrackReference.Clips.Add(controlClip);
#endif
                    
                    bindings.Add(reference);
                }
                
                bindings.Add(controlTrackReference);*/
            }
        }

        public void CleanReferences(PlayableDirector director,PlayableAsset animation)
        {
            /*foreach (var clip in animation
                         .GetClips<ControlPlayableAsset,ControlTrack>())
            {
                clip.prefabGameObject = null;
#if UNITY_EDITOR
                clip.MarkDirty();
                animation.MarkDirty();
#endif
            }*/
        }
    }
    
    
}