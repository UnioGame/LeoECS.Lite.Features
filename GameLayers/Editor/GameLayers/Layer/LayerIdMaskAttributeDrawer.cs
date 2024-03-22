namespace Game.Code.Configuration.Editor.GameLayers.Layer
{
    using System;
    using System.Linq;
    using Code.GameLayers.Layer;
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(LayerIdMaskAttribute))]
    public sealed class LayerIdMaskAttributeDrawer : PropertyDrawer
    {
        private LayerIdConfiguration _configuration;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Initialize();
            
            EditorGUI.BeginProperty(position, label, property);
            
            var propertyValue = property.FindPropertyRelative("_value");
            var layers = _configuration == null ? Array.Empty<string>() : _configuration.LayerNames.Where(x=> !string.IsNullOrEmpty(x)).ToArray();
            
            propertyValue.intValue = EditorGUI.MaskField(position, label, propertyValue.intValue, layers);
            property.serializedObject.ApplyModifiedProperties();
            
            EditorGUI.EndProperty();
        }
        
        private void Initialize()
        {
            if(_configuration != null)
                return;

            var assetGuid = AssetDatabase.FindAssets($"t:{nameof(LayerIdConfiguration)}").FirstOrDefault();
            if(string.IsNullOrEmpty(assetGuid))
                return;

            var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
            if(string.IsNullOrEmpty(assetPath))
                return;

            _configuration = AssetDatabase.LoadAssetAtPath<LayerIdConfiguration>(assetPath);
        }
    }
}