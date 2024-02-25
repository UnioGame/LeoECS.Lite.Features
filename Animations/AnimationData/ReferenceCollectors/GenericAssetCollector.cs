namespace Game.Code.Animations.ReferenceCollectors
{
    using System;
    using Resolvers;
    using UnityEngine;
    using UnityEngine.Playables;
    using Object = UnityEngine.Object;

    [Serializable]
    public class GenericAssetCollector : IPlayableReferenceCollector
    {
        public void CollectReferences(PlayableBindingData map, PlayableAsset animation,  PlayableDirector playableDirector)
        {
            foreach (var animationOutput in animation.outputs)
            {
                var key = animationOutput.sourceObject;
                var bindings = map.bindings;

                var target = playableDirector.GetGenericBinding(key);
                if (target == null || target is GameObject or Component) continue;
                
                var monoReference = new AssetObjectReference()
                {
                    asset = target
                };

                bindings.Add(monoReference);
            }
        }
    }
}