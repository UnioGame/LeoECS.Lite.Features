namespace Game.Code.DataBase.Runtime
{
    using System;
    using Abstract;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    [Serializable]
    public class AddressablesObjectRecord : IGameDatabaseRecord
    {
        [SerializeField]
        public AssetReference assetReference;

        public string name;

        public string Id => assetReference.AssetGUID;

        public string Name
        {
            get
            {
#if UNITY_EDITOR
                var asset = assetReference.editorAsset;
                name = asset == null ? string.Empty : asset.name;
                return name;
#endif
                return name;    
            }
        }

        public bool IsMatch(string searchString)
        {
            if (string.IsNullOrEmpty(searchString)) return true;
            if (Id != null && Id.IndexOf(searchString,StringComparison.OrdinalIgnoreCase) >= 0) return true;
            if (Name != null && Name.IndexOf(searchString,StringComparison.OrdinalIgnoreCase) >= 0) return true;
            return false;
        }
    }
}
