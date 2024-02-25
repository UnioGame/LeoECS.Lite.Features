namespace Game.Ecs.Effects.Data
{
    using System;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [Serializable]
    public class EffectRootKey
    {
        [Tooltip("root name for editor usage")]
        public string name = string.Empty;
        [Tooltip("name of target object in game object hierarchy")]
        public string objectName;
        [Tooltip("is target object is child of root object")]
        public bool isChild = true;
    }
}