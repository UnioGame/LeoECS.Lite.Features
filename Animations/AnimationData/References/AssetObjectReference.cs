namespace Game.Code.Animations.Resolvers
{
    using System;
    using Object = UnityEngine.Object;

    [Serializable]
    public class AssetObjectReference : IPlayableReference
    {
        public Object asset;
    }
}