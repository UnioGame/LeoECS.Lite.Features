namespace Game.Code.Animations.Resolvers
{
    using System;
    using UniGame.Core.Runtime.SerializableType;
    using Object = UnityEngine.Object;

    [Serializable]
    public class MonoObjectReference : IPlayableReference
    {
        public Object Key;
        public string Name = string.Empty;
        public bool IsGameObject;
        public bool Child = true;
        public SType AssetType;
    }
}