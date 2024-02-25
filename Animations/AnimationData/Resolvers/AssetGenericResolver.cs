namespace Game.Code.Animations
{
    using System;
    using Cysharp.Threading.Tasks;
    using Resolvers;
    using UnityEngine.Playables;
    using Object = UnityEngine.Object;

    [Serializable]
    public class AssetGenericResolver : IPlayableReferenceResolver
    {
        public void Resolve(IPlayableReference reference, PlayableDirector director)
        {
            if (reference is not AssetObjectReference objectReference)
                return;
        }
    }
}