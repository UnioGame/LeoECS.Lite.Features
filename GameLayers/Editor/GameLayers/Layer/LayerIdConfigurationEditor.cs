namespace Game.Code.Configuration.Editor.GameLayers.Layer
{
    using Code.GameLayers.Layer;
    using UnityEditor;
    using UnityEditorInternal;
    using UnityEngine;

    [CustomEditor(typeof(LayerIdConfiguration))]
    public sealed class LayerIdConfigurationEditor : Editor
    {
        private SerializedProperty _layersProperty;
        private ReorderableList _layersList;
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            _layersList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            _layersProperty = serializedObject.FindProperty("_layers");
            if (_layersProperty != null)
            {
                _layersList = new ReorderableList(serializedObject, _layersProperty, false, false, false, false)
                {
                    drawElementCallback = DrawLayerListElement, 
                    elementHeight = EditorGUIUtility.singleLineHeight + 2.0f, 
                    headerHeight = 3.0f
                };
            }
        }

        private void DrawLayerListElement(Rect rect, int index, bool selected, bool focused)
        {
            ++rect.yMin;
            --rect.yMax;

            var stringValue = _layersProperty.GetArrayElementAtIndex(index).stringValue;
            var content = EditorGUI.TextField(rect, $"Layer {index}", stringValue);
            if (content != stringValue)
                _layersProperty.GetArrayElementAtIndex(index).stringValue = content;
        }
    }
}