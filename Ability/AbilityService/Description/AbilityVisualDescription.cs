namespace Game.Code.Configuration.Runtime.Ability.Description
{
    using System;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Serialization;

    [Serializable]
    public sealed class AbilityVisualDescription
    {
        [FormerlySerializedAs("_name")] 
        [SerializeField]
        public string name;
        
        [FormerlySerializedAs("_description")] 
        [SerializeField]
        public string description;
        
        [SerializeField]
        public string manaCost;

        [FormerlySerializedAs("_icon")] 
        [SerializeField]
        public AssetReferenceT<Sprite> icon;

        public string Name => name;
        
        public string Description => description;

        public AssetReferenceT<Sprite> Icon => icon;
    }
}