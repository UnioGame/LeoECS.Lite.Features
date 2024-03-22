namespace Game.Code.Configuration.Editor.GameLayers.Category
{
    using System.Linq;
    using Code.GameLayers.Category;
    using EditorExtensions;
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(CategoryId))]
    public sealed class CategoryIdDrawer : PropertyDrawer
    {
        private static readonly int CategoryIdHash = nameof(CategoryIdHash).GetHashCode();
        
        private CategoryIdConfiguration _configuration;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Initialize();

            EditorGUI.BeginProperty(position, label, property);

            var propertyValue = property.FindPropertyRelative("_value");
            propertyValue.intValue = CategoryField(position, label, propertyValue.intValue, EditorStyles.popup);
            property.serializedObject.ApplyModifiedProperties();

            EditorGUI.EndProperty();
        }

        private int CategoryField(Rect position, GUIContent label, int category, GUIStyle style)
        {
            var controlId = GUIUtility.GetControlID(CategoryIdHash, FocusType.Keyboard, position);
            position = EditorGUI.PrefixLabel(position, controlId, label);

            if (_configuration == null)
                return category;
            
            var current = Event.current;
            var changed = GUI.changed;
            
            var categories = _configuration.Categories.Where(x=> !string.IsNullOrEmpty(x)).ToArray();
            var selectedValueForControl = EditorGUIExtensions.GetSelectedValueForControl(controlId, -1);
            
            if (selectedValueForControl != -1)
            {
                if (selectedValueForControl >= categories.Length)
                {
                    EditorGUIUtility.PingObject(_configuration);
                    Selection.activeObject = _configuration;

                    GUI.changed = changed;
                }
                else
                {
                    var selected = 0;
                    for (var i = 0; i < CategoryIdConfiguration.MaxCategoryCount; i++)
                    {
                        if (string.IsNullOrEmpty(_configuration.GetCategoryName(i)))
                            continue;

                        if (selected == selectedValueForControl)
                        {
                            category = _configuration.GetCategoryValue(i);
                            break;
                        }

                        selected++;
                    }
                }
            }

            if (current.type == EventType.MouseDown && position.Contains(current.mousePosition) || EditorGUIExtensions.MainActionKeyForControl(current, controlId))
            {
                var selected = 0;
                for (var i = 0; i < CategoryIdConfiguration.MaxCategoryCount; i++)
                {
                    if (!string.IsNullOrEmpty(_configuration.GetCategoryName(i)))
                    {
                        if (_configuration.GetCategoryValue(i) != category)
                            selected++;
                        else
                            break;
                    }
                }

                
                ArrayUtility.Add(ref categories, "");
                ArrayUtility.Add(ref categories, "Add Category...");

                EditorGUIExtensions.DoPopup(position, controlId, selected, EditorGUIExtensions.TempContent(categories), style);
                current.Use();

                return category;
            }

            if (current.type == EventType.Repaint)
            {
                var content = EditorGUI.showMixedValue ? EditorGUIUtility.TrTextContent("—", "Mixed Values") : EditorGUIExtensions.TempContent(_configuration.GetCategoryNameByValue(category));
                style.Draw(position, content, controlId, false, position.Contains(current.mousePosition));
            }

            return category;
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