namespace Game.Code.Animations
{
    using System;
    using Resolvers;
    using UnityEngine.Playables;
    using Object = UnityEngine.Object;

    [Serializable]
    public abstract class OutputTrackResolver<TTrack,TData> : IPlayableReferenceResolver
        where TTrack : Object
        where TData : IPlayableReference
    {
        public void Resolve(IPlayableReference reference, PlayableDirector director)
        {
            if (director == null) return;
            
            var animation = director.playableAsset;
            if(animation == null) return;
            
            if (reference is not TData controlReference) return;
            
            foreach (var output in animation.outputs)
            {
                var source = output.sourceObject;
                if(source is not TTrack trackAsset) continue;
                
                OnResolve(director,trackAsset,controlReference);
            }
            
        }

        protected virtual void OnResolve(PlayableDirector director, TTrack trackAsset, TData controlReference)
        {
            
        }
    }
}