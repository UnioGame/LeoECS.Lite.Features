namespace Game.Code.Animations.ReferenceCollectors
{
    using System;
    using Abstract;
    using Modules.UnioModules.UniGame.CoreModules.UniGame.Core.Runtime.Extension;
    using Resolvers;
    using UnityEngine;
    using UnityEngine.Playables;

    [Serializable]
    public class GenericReferenceCollector : 
        IPlayableReferenceCollector,
        IPlayableReferenceCleaner
    {
        public void CollectReferences(PlayableBindingData map, PlayableAsset animation, PlayableDirector playableDirector)
        {
            foreach (var animationOutput in animation.outputs)
            {
                var key = animationOutput.sourceObject;
                var bindings = map.bindings;
                
                var target = playableDirector.GetGenericBinding(key);
                var gameObject = target as GameObject;
                var component = target as Component;
            
                if(gameObject == null && component == null) continue;

                var directorObject = playableDirector.gameObject;
                
                var isRoot = component!=null && component.gameObject == directorObject || 
                             gameObject == directorObject;
                
                var targetName = isRoot ? BindConstants.Self : target.name;
                var type = target.GetType();
                var isChild = !isRoot && playableDirector.gameObject.FindChildGameObject(targetName)!=null;

                var monoReference = new MonoObjectReference()
                {
                    Key = key,
                    AssetType = type,
                    Name = targetName,
                    IsGameObject = gameObject!=null,
                    Child = isChild
                };
            
                bindings.Add(monoReference);
            }
            
        }

        public void CleanReferences(PlayableDirector director, PlayableAsset animation)
        {
            foreach (var animationOutput in animation.outputs)
            {
                director.ClearGenericBinding(animationOutput.sourceObject);
            }
        }

    }
}