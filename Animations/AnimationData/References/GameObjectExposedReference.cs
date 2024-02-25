namespace Game.Code.Animations.PlayableBindings
{
    using System;
    using Resolvers;
    using UnityEngine.Serialization;

    [Serializable]
    public class GameObjectExposedReference : IPlayableReference
    {
        public int Id;
        public string Name = string.Empty;
        public bool Child = true;
        public string Guid = string.Empty;
    }
}