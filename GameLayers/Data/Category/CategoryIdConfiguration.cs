namespace Game.Code.GameLayers.Category
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "Category Id Configuration", menuName = "Game/Configurations/Game Layers/Category Id Configuration")]
    public sealed class CategoryIdConfiguration : ScriptableObject
    {
        public const int MaxCategoryCount = 32;
        
        [SerializeField]
        private string[] _categories = new string[MaxCategoryCount];
        
        public IReadOnlyList<string> Categories => _categories;
        
        public string GetCategoryName(int categoryIndex)
        {
            if (categoryIndex < 0 || categoryIndex >= _categories.Length)
                return string.Empty;

            return _categories[categoryIndex];
        }

        public string GetCategoryNameByValue(int categoryValue)
        {
            var index = (int)Mathf.Log(categoryValue, 2);
            return index >= _categories.Length || index < 0 ? string.Empty : _categories[index];
        }

        public int GetCategoryValue(int categoryIndex)
        {
            if (categoryIndex < 0 || categoryIndex >= _categories.Length)
                return 0;

            return (int)Mathf.Pow(2, categoryIndex);
        }
    }
}