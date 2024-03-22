namespace Game.Code.Configuration.Editor.GameLayers.Category
{
    using System;
    using System.Linq;
    using Code.GameLayers.Category;
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(CategoryIdMaskAttribute))]
    public sealed class CategoryIdMaskAttributeDrawer : PropertyDrawer
    {
        private CategoryIdConfiguration _configuration;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Initialize();
            
            EditorGUI.BeginProperty(position, label, property);
            
            var propertyValue = property.FindPropertyRelative("_value");
            var categories = _configuration == null ? Array.Empty<string>() : _configuration.Categories.Where(x=> !string.IsNullOrEmpty(x)).ToArray();
            
            propertyValue.intValue = EditorGUI.MaskField(position, label, propertyValue.intValue, categories);
            property.serializedObject.ApplyModifiedProperties();
            
            EditorGUI.EndProperty();
        }
        
        private void Initialize()
        {
            if(_configuration != null)
                return;

            var assetGuid = AssetDatabase.FindAssets($"t:{nameof(CategoryIdConfiguration)}").FirstOrDefault();
            if(string.IsNullOrEmpty(assetGuid))
                return;

            var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
            if(string.IsNullOrEmpty(assetPath))
                return;

            _configuration = AssetDatabase.LoadAssetAtPath<CategoryIdConfiguration>(assetPath);
        }
    }
}