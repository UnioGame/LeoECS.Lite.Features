namespace Game.Code.Configuration.Editor.GameLayers.Relationship
{
    using System;
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
        
        private LayerId[] _layerIdConfiguration;
        private RelationshipId[] _relationshipIdConfiguration;

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
            _layerIdConfiguration = Enum.GetValues(typeof(LayerId)) as LayerId[];
            _relationshipIdConfiguration = Enum.GetValues(typeof(RelationshipId)) as RelationshipId[];

            if(_layerIdConfiguration == null || _relationshipIdConfiguration == null)
                return;

            _relationshipIdMap = serializedObject.targetObject as RelationshipIdMap;
            if(_relationshipIdMap == null)
                return;
            
            _matrix = _relationshipIdMap.RelationshipMatrix;

            var layersCount = _layerIdConfiguration.Length;
            layersCount = Mathf.Min(layersCount, RelationshipIdMap.MaxRowAndColumnCount);
            
            _table = GUITable.Create(layersCount, layersCount, DrawTableElement, "Layers", DrawColumnLabel, "Layers", DrawRowLabel);
        }

        private void DrawRowLabel(Rect position, int x)
        {
            var layerIndex = _layerIdConfiguration[x];
            var layerName = layerIndex.ToString();
            
            GUI.Label(position, layerName);
        }

        private void DrawColumnLabel(Rect position, int y)
        {
            var layerIndex = _layerIdConfiguration[y];
            var layerName = layerIndex.ToString();
            
            GUI.Label(position, layerName);
        }

        private void DrawTableElement(Rect position, int x, int y)
        {
            EditorGUI.BeginChangeCheck();
            _matrix[x, y] = (RelationshipId)RelationshipIdDrawer.RelationshipField(position, GUIContent.none, (int)_matrix[x, y], _relationshipIdConfiguration);
            if (EditorGUI.EndChangeCheck())
            {
                _relationshipIdMap.RelationshipMatrix = _matrix;
                EditorUtility.SetDirty(_relationshipIdMap);
            }
        }
    }
}