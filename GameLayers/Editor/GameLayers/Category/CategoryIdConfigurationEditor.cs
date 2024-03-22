namespace Game.Code.Configuration.Editor.GameLayers.Category
{
    using Code.GameLayers.Category;
    using UnityEditor;
    using UnityEditorInternal;
    using UnityEngine;

    [CustomEditor(typeof(CategoryIdConfiguration))]
    public sealed class CategoryIdConfigurationEditor : Editor
    {
        private SerializedProperty _categoriesProperty;
        private ReorderableList _categoriesList;
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            _categoriesList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            _categoriesProperty = serializedObject.FindProperty("_categories");
            if (_categoriesProperty != null)
            {
                _categoriesList = new ReorderableList(serializedObject, _categoriesProperty, false, false, false, false)
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

            var stringValue = _categoriesProperty.GetArrayElementAtIndex(index).stringValue;
            var content = EditorGUI.TextField(rect, $"Category {index}", stringValue);
            if (content != stringValue)
                _categoriesProperty.GetArrayElementAtIndex(index).stringValue = content;
        }
    }
}