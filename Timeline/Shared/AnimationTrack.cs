namespace Game.Code.Timeline.Shared
{
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Playables;
    using UnityEngine.Timeline;

    public abstract class AnimationTrack<TBinding, TValueMixer, TAnimationMixerBehaviour, TAnimationBehaviour> : 
        TrackAsset
        where TAnimationMixerBehaviour : AnimationMixerBehaviour<TBinding, TValueMixer, TAnimationBehaviour>, new()
        where TValueMixer : AnimationMixer<TBinding, TAnimationBehaviour>, new()
        where TBinding : class
        where TAnimationBehaviour : AnimationBehaviour, new()
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<TAnimationMixerBehaviour>.Create(graph, inputCount);
        }

        public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
        {
#if UNITY_EDITOR
            if (director.GetGenericBinding(this) is not TBinding binding)
                return;

            if (binding is not Object asset) return;
            
            var so = new SerializedObject(asset);
            
            var iterator = so.GetIterator();
            while (iterator.NextVisible(true))
            {
                if (iterator.hasVisibleChildren)
                    continue;
                
                var gameAsset = binding is GameObject gameObject 
                    ? gameObject
                    : binding is Component component
                        ? component.gameObject
                        : null;

                if(gameAsset==null) continue;
                
                driver.AddFromName(gameAsset, iterator.propertyPath);
            }
#endif
            base.GatherProperties(director, driver);
        }
    }
}