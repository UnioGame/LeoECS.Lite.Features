namespace Game.Code.Animations.PlayableBindings
{
    using System;
    using Modules.UnioModules.UniGame.CoreModules.UniGame.Core.Runtime.Extension;
    using Resolvers;
    using UnityEngine;
    using UnityEngine.Playables;

    [Serializable]
    public class GameObjectSearchResolver : IPlayableReferenceResolver
    {
        public void Resolve(IPlayableReference reference, PlayableDirector director)
        {
            if (reference is not GameObjectExposedReference objectReference)
                return;
            
            if(string.IsNullOrEmpty(objectReference.Name))
                return;
            
            var gameObject = director.gameObject;
            var isChild = objectReference.Child;
            
            var resultObject = isChild 
                ? gameObject.FindChildGameObject(objectReference.Name)
                : GameObject.Find(objectReference.Name);
            
            if(resultObject == null) return;
            
            director.SetReferenceValue(objectReference.Id,resultObject);
        }
    }
}