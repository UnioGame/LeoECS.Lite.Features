namespace Game.Code.Animations.Abstract
{
    using UnityEngine.Playables;

    public interface IPlayableReferenceCleaner
    {
        void CleanReferences(PlayableDirector director,PlayableAsset animation);
    }
}