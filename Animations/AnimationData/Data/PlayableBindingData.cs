namespace Game.Code.Animations
{
    using System;
    using System.Collections.Generic;
    using Resolvers;
    using UnityEngine;

    [Serializable]
    public class PlayableBindingData
    {
        [SerializeReference]
        public List<IPlayableReference> bindings = new List<IPlayableReference>();
        
        public void Clear()
        {
            bindings.Clear();
        }
    }
}