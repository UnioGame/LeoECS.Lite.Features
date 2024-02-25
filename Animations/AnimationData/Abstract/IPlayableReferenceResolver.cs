namespace Game.Code.Animations
{
    using Resolvers;
    using UnityEngine.Playables;

    public interface IPlayableReferenceResolver
    {
        void Resolve(IPlayableReference reference,PlayableDirector director);
    }
}