namespace Game.Code.Animations
{
    using System.Collections.Generic;
    using Abstract;
    using PlayableBindings;
    using ReferenceCollectors;
    using UnityEngine.Playables;

#if UNITY_EDITOR
    using UniModules.Editor;
#endif
    
    public class PlayableBinder
    {
        
        public readonly List<IPlayableReferenceCollector> Collectors = new()
        {
            new ControlTrackReferencesCollector(),
            new AnimationTrackCollector(),
            new GenericReferenceCollector(),
            new GenericAssetCollector(),
        };
        
        public readonly List<IPlayableReferenceResolver> Resolvers =
            new()
            {
                new GameObjectSearchResolver(),
                new AssetGenericResolver(),
                new GameObjectGenericResolver(),
                new ControlTrackResolver(),
                new AnimationTrackResolver(),
            };

        public void BakeAnimationBinding(
            PlayableDirector playableDirector,
            PlayableAsset animation,
            PlayableBindingData data)
        {
#if UNITY_EDITOR
            if (playableDirector == null || animation == null || data == null) return;

            var defaultPlayable = playableDirector.playableAsset;
            playableDirector.playableAsset = animation;
            
            var bindingData = data;
            bindingData.Clear();
            
            foreach (var generalCollector in Collectors)
            {
                generalCollector.CollectReferences(bindingData,animation,playableDirector);
            }

            playableDirector.playableAsset = defaultPlayable;
            playableDirector.gameObject.MarkDirty();
            playableDirector.MarkDirty();
#endif
        }
        
        
        public void ResolveBindings(PlayableDirector playableDirector, AnimationLink animationLink)
        {
            var animation = animationLink.animation;
            var bindingData = animationLink.bindingData;
            ResolveBindings(playableDirector,bindingData);
        }
        
        public void ResolveBindings(
            PlayableDirector playableDirector,
            PlayableBindingData data)
        {
            if(data == null) return;
            
            var bindings = data.bindings;

            foreach (var resolver in Resolvers)
            {
                foreach (var reference in bindings)
                {
                    resolver.Resolve(reference, playableDirector);
                }
            }
        }


        public void ClearReferences(PlayableDirector playableDirector,PlayableAsset animation)
        {
            foreach (var collector in Collectors)
            {
                if(collector is not IPlayableReferenceCleaner cleaner)
                    continue;
                cleaner.CleanReferences(playableDirector,animation);
            }
        }
        
    }
}