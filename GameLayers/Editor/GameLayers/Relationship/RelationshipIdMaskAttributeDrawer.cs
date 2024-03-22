namespace Game.Code.Configuration.Editor.GameLayers.Relationship
{
    using System;
    using System.Linq;
    using Code.GameLayers.Relationship;
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(RelationshipIdMaskAttribute))]
    public sealed class RelationshipIdMaskAttributeDrawer : PropertyDrawer
    {
        private RelationshipIdConfiguration _configuration;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Initialize();
            
            EditorGUI.BeginProperty(position, label, property);
            
            var propertyValue = property.FindPropertyRelative("_value");
            var relationships = _configuration == null ? Array.Empty<string>() : _configuration.Relationships.Where(x=> !string.IsNullOrEmpty(x)).ToArray();
            
            propertyValue.intValue = EditorGUI.MaskField(position, label, propertyValue.intValue, relationships);
            property.serializedObject.ApplyModifiedProperties();
            
            EditorGUI.EndProperty();
        }
        
        private void Initialize()
        {
            if(_configuration != null)
                return;

            var assetGuid = AssetDatabase.FindAssets($"t:{nameof(RelationshipIdConfiguration)}").FirstOrDefault();
            if(string.IsNullOrEmpty(assetGuid))
                return;

            var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
            if(string.IsNullOrEmpty(assetPath))
                return;

            _configuration = AssetDatabase.LoadAssetAtPath<RelationshipIdConfiguration>(assetPath);
        }
    }
}