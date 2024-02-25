namespace Game.Code.DataBase.Runtime
{
    using System;
    using UnityEngine.AddressableAssets;

    [Serializable]
    public class AssetReferenceGameDataCategory : AssetReferenceT<GameDataCategory>
    {
        public AssetReferenceGameDataCategory(string guid) : base(guid)
        {
        }
    }
}