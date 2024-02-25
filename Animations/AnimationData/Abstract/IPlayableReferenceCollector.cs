namespace Game.Code.Animations
{
    using UnityEngine.Playables;

    public interface IPlayableReferenceCollector
    {
        public void CollectReferences(PlayableBindingData map, PlayableAsset animation,  PlayableDirector director);
    }
}