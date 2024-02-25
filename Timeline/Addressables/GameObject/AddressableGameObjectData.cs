namespace Game.Code.Timeline.Addressables
{
    using System;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    [Serializable]
    public class AddressableGameObjectData
    {
        [DrawWithUnity]
        public AssetReferenceGameObject asset;
        
        public bool makeInstance;
        
        [ShowIf(nameof(makeInstance))]
        public bool useParent = false;
        
        [ShowIf(nameof(makeInstance))]
        public Vector3 position;
        
        [ShowIf(nameof(makeInstance))]
        public Vector3 rotation;
        
        [ShowIf(nameof(makeInstance))]
        public Vector3 scale = new Vector3(1f,1f,1f);
        
        [ShowIf(nameof(makeInstance))]
        public bool stayWorldPosition = false;
    }
}