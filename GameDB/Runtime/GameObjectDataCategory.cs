namespace Game.Code.DataBase.Runtime
{
    using System.Collections.Generic;
    using Abstract;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

#if UNITY_EDITOR
    using UniModules.UniGame.AddressableExtensions.Editor;
    using UniModules.Editor;
#endif

    [CreateAssetMenu(menuName = "Game/Game Data/GameObject Category", fileName = "GameObject Category Asset")]
    public class GameObjectDataCategory : GameDataCategory, IGameDataCategory
    {
        public const string SettingsGroupKey = "settings";
        
        #region inspector

        [Searchable(FilterOptions = SearchFilterOptions.ISearchFilterableInterface)]
        public List<AddressablesObjectRecord> records = new List<AddressablesObjectRecord>();

        
        [BoxGroup(SettingsGroupKey)]
        [FolderPath] 
        public List<string> folders = new List<string>();

        #endregion

        public override IReadOnlyList<IGameDatabaseRecord> Records => records;

#if UNITY_EDITOR

        [Button(ButtonSizes.Large,Icon = SdfIconType.ArchiveFill)]
        public override void FillCategory()
        {
            var assets = GetAssets(folders.ToArray());
            FillCategory(assets);
        }

        public void FillCategory(IEnumerable<GameObject> assets)
        {
            records.Clear();
            
            foreach (var asset in assets)
            {
                if(!asset.IsInAnyAddressableAssetGroup())continue;
                if(!Validate(asset))continue;
                
                var entry = new AddressablesObjectRecord()
                {
                    name = asset.name,
                    assetReference = new AssetReference(asset.GetGUID())
                };
                
                records.Add(entry);
            }
        }
        
        public virtual bool Validate(GameObject asset)
        {
            return true;
        }

        public virtual IEnumerable<GameObject> GetAssets(string[] targetFolders)
        {
            var assets = AssetEditorTools.GetAssets<GameObject>(targetFolders);
            return assets;
        }

#endif
    }
}