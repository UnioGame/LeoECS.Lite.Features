namespace Game.Code.DataBase.Runtime
{
    using System.Collections.Generic;
    using System.Linq;
    using Sirenix.OdinInspector;
    using UnityEngine;
    
#if UNITY_EDITOR
    using UnityEditor;
    using UniModules.Editor;
#endif

    [CreateAssetMenu(menuName = "Game/GameDatabase/"+nameof(GameDataBaseAsset),fileName = nameof(GameDataBaseAsset))]
    public class GameDataBaseAsset : ScriptableObject
    {

        [SerializeField]
        [InlineProperty]
        [HideLabel]
        public GameDatabase gameDatabase;
        
#if UNITY_EDITOR

        public static IEnumerable<ValueDropdownItem<GameResourceRecordId>> GetGameRecordIds(GameResourceCategoryId category)
        {
            var assets = AssetDatabase.FindAssets($"t:{nameof(GameDataBaseAsset)}");
            var guid = assets.FirstOrDefault();
            if(string.IsNullOrEmpty(guid))
               yield break;

            var config = AssetDatabase.LoadAssetAtPath<GameDataBaseAsset>(AssetDatabase.GUIDToAssetPath(guid));

            yield return new ValueDropdownItem<GameResourceRecordId>()
            {
                Text = "EMPTY",
                Value = GameResourceRecordId.Empty
            };
            
            var ids = config.gameDatabase
                .categories
                .Where(x => x.editorAsset!=null)
                .Where(x => 
                    x.editorAsset.category == GameResourceCategoryId.Empty || 
                    x.editorAsset.category == category)
                .Select(x => x.editorAsset)
                .SelectMany(x => x.Records)
                .Select(x => new ValueDropdownItem<GameResourceRecordId>() {
                    Text = x.Name,
                    Value = (GameResourceRecordId)x.Id, }).ToList();

            foreach (var id in ids)
                yield return id;
        }

        
        public static IEnumerable<ValueDropdownItem<GameResourceRecordId>> GetGameRecordIds()
        {
            return GetGameRecordIds(GameResourceCategoryId.Empty);            
        }
        
        public static IEnumerable<GameResourceCategoryId> GetGameRecordCategories()
        {
            var assets = AssetDatabase.FindAssets($"t:{nameof(GameDataBaseAsset)}");
            var guid = assets.FirstOrDefault();
            if(string.IsNullOrEmpty(guid))
                yield break;

            yield return GameResourceCategoryId.Empty;
            
            var config = AssetDatabase.LoadAssetAtPath<GameDataBaseAsset>(AssetDatabase.GUIDToAssetPath(guid));
            var records = config.gameDatabase
                .categories
                .Where(x => x.editorAsset!=null)
                .Select(x => x.editorAsset)
                .Select(x => (GameResourceCategoryId)x.Category)
                .ToList();

            foreach (var record in records)
                yield return record;
        }

        [Button(Icon = SdfIconType.ArchiveFill)]
        [PropertyOrder(-1)]
        public void UpdateData()
        {
            foreach (var category in gameDatabase.categories)
            {
                var categoryAsset = category.editorAsset;
                if(categoryAsset == null) continue;
                categoryAsset.FillCategory();
                categoryAsset.MarkDirty();
            }
        }
        
#endif

    }
}
