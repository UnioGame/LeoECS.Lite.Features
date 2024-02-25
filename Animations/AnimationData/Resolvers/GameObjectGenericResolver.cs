namespace Game.Code.Animations
{
    using System;
    using Cysharp.Threading.Tasks;
    using Modules.UnioModules.UniGame.CoreModules.UniGame.Core.Runtime.Extension;
    using Resolvers;
    using UniGame.Core.Runtime.Extension;
    using UnityEngine.Playables;
    using Object = UnityEngine.Object;

    [Serializable]
    public class GameObjectGenericResolver : IPlayableReferenceResolver
    {
        public void Resolve(IPlayableReference reference, PlayableDirector director)
        {
            if (reference is not MonoObjectReference objectReference) return;
            
            var name = objectReference.Name;
            if(string.IsNullOrEmpty(name)) return;
            
            var source = director.gameObject;
            var gameObject = name.Equals(BindConstants.Self)
                ? director.gameObject                
                : source.FindGameObject(objectReference.Name, objectReference.Child);
            
            if(gameObject == null) return;
            
            var assetType = (Type)objectReference.AssetType;
            var isComponent = assetType.IsComponent();
            
            Object result = isComponent
                ? gameObject.GetComponent(assetType)
                : gameObject;
           
            director.SetGenericBinding(objectReference.Key, result);
        }
    }
}