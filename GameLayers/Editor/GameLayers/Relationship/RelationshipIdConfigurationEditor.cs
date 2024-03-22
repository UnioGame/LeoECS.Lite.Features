namespace Game.Code.Configuration.Editor.GameLayers.Relationship
{
    using System.Linq;
    using Code.GameLayers.Relationship;
    using UnityEditor;
    using UnityEditorInternal;
    using UnityEngine;

    [CustomEditor(typeof(RelationshipIdConfiguration))]
    public sealed class RelationshipIdConfigurationEditor : Editor
    {
        private SerializedProperty _relationshipsProperty;
        private ReorderableList _relationshipsList;

        private RelationshipIdMap _relationshipIdMap;
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            _relationshipsList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
            
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUI.enabled = _relationshipIdMap != null;
            if (GUILayout.Button("Configure map...", GUILayout.Width(110.0f), GUILayout.Height(22.0f)))
            {
                EditorGUIUtility.PingObject(_relationshipIdMap);
                Selection.activeObject = _relationshipIdMap;
            }
            GUI.enabled = true;
            GUILayout.EndHorizontal();
        }

        private void OnEnable()
        {
            _relationshipsProperty = serializedObject.FindProperty("_relationships");
            if (_relationshipsProperty != null)
            {
                _relationshipsList = new ReorderableList(serializedObject, _relationshipsProperty, false, false, false, false)
                {
                    drawElementCallback = DrawLayerListElement, 
                    elementHeight = EditorGUIUtility.singleLineHeight + 2.0f, 
                    headerHeight = 3.0f
                };
            }
            
            var assetGuid = AssetDatabase.FindAssets($"t:{nameof(RelationshipIdMap)}").FirstOrDefault();
            if(string.IsNullOrEmpty(assetGuid))
                return;

            var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
            if(string.IsNullOrEmpty(assetPath))
                return;

            _relationshipIdMap = AssetDatabase.LoadAssetAtPath<RelationshipIdMap>(assetPath);
        }

        private void DrawLayerListElement(Rect rect, int index, bool selected, bool focused)
        {
            ++rect.yMin;
            --rect.yMax;

            var stringValue = _relationshipsProperty.GetArrayElementAtIndex(index).stringValue;
            var content = EditorGUI.TextField(rect, $"Relationship {index}", stringValue);
            if (content != stringValue)
                _relationshipsProperty.GetArrayElementAtIndex(index).stringValue = content;
        }
    }
}