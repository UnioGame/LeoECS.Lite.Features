namespace Game.Code.Configuration.Editor.GameLayers.Relationship
{
    using System.Linq;
    using Code.GameLayers.Layer;
    using Code.GameLayers.Relationship;
    using Sirenix.Utilities.Editor;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(RelationshipIdMap))]
    public sealed class RelationshipIdMapEditor : Editor
    {
        private GUITable _table;
        
        private LayerIdConfiguration _layerIdConfiguration;
        private RelationshipIdConfiguration _relationshipIdConfiguration;

        private RelationshipIdMap _relationshipIdMap;

        private RelationshipId[,] _matrix;
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            if(_layerIdConfiguration == null || _table == null)
                return;
            
            _table.DrawTable();

            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            LoadLayerConfiguration();
            LoadRelationshipConfiguration();

            if(_layerIdConfiguration == null || _relationshipIdConfiguration == null)
                return;

            _relationshipIdMap = serializedObject.targetObject as RelationshipIdMap;
            if(_relationshipIdMap == null)
                return;
            
            _matrix = _relationshipIdMap.RelationshipMatrix;

            var layersCount = _layerIdConfiguration.LayerNames.Count(x => !string.IsNullOrEmpty(x));
            layersCount = Mathf.Min(layersCount, RelationshipIdMap.MaxRowAndColumnCount);
            
            _table = GUITable.Create(layersCount, layersCount, DrawTableElement, "Layers", DrawColumnLabel, "Layers", DrawRowLabel);
        }

        private void DrawRowLabel(Rect position, int x)
        {
            var layerIndex = FindNextNotEmptyLayer(x);
            var layerName = _layerIdConfiguration.GetLayerName(layerIndex);
            
            GUI.Label(position, layerName);
        }

        private void DrawColumnLabel(Rect position, int y)
        {
            var layerIndex = FindNextNotEmptyLayer(y);
            var layerName = _layerIdConfiguration.GetLayerName(layerIndex);
            
            GUI.Label(position, layerName);
        }

        private void DrawTableElement(Rect position, int x, int y)
        {
            EditorGUI.BeginChangeCheck();
            _matrix[x, y] = (RelationshipId)RelationshipIdDrawer.RelationshipField(position, GUIContent.none, _matrix[x, y], _relationshipIdConfiguration);
            if (EditorGUI.EndChangeCheck())
            {
                _relationshipIdMap.RelationshipMatrix = _matrix;
                EditorUtility.SetDirty(_relationshipIdMap);
            }
        }

        private int FindNextNotEmptyLayer(int index)
        {
            for (var i = index; i < _layerIdConfiguration.LayerNames.Count; i++)
            {
                if(string.IsNullOrEmpty(_layerIdConfiguration.GetLayerName(i)))
                    continue;

                return i;
            }

            return index;
        }

        private void LoadLayerConfiguration()
        {
            var assetGuid = AssetDatabase.FindAssets($"t:{nameof(LayerIdConfiguration)}").FirstOrDefault();
            if(string.IsNullOrEmpty(assetGuid))
                return;

            var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
            if(string.IsNullOrEmpty(assetPath))
                return;

            _layerIdConfiguration = AssetDatabase.LoadAssetAtPath<LayerIdConfiguration>(assetPath);
        }
        
        private void LoadRelationshipConfiguration()
        {
            var assetGuid = AssetDatabase.FindAssets($"t:{nameof(RelationshipIdConfiguration)}").FirstOrDefault();
            if(string.IsNullOrEmpty(assetGuid))
                return;

            var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
            if(string.IsNullOrEmpty(assetPath))
                return;

            _relationshipIdConfiguration = AssetDatabase.LoadAssetAtPath<RelationshipIdConfiguration>(assetPath);
        }
    }
}