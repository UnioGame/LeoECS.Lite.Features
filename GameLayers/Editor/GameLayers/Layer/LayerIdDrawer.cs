namespace Game.Code.Configuration.Editor.GameLayers.Layer
{
    using System.Linq;
    using Code.GameLayers.Layer;
    using EditorExtensions;
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(LayerId))]
    public sealed class LayerIdDrawer : PropertyDrawer
    {
        private static readonly int LayerIdHash = nameof(LayerIdHash).GetHashCode();
        
        private LayerIdConfiguration _configuration;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Initialize();

            EditorGUI.BeginProperty(position, label, property);

            var propertyValue = property.FindPropertyRelative("_value");
            propertyValue.intValue = LayerField(position, label, propertyValue.intValue, EditorStyles.popup);
            property.serializedObject.ApplyModifiedProperties();

            EditorGUI.EndProperty();
        }

        private int LayerField(Rect position, GUIContent label, int layer, GUIStyle style)
        {
            var controlId = GUIUtility.GetControlID(LayerIdHash, FocusType.Keyboard, position);
            position = EditorGUI.PrefixLabel(position, controlId, label);

            if (_configuration == null)
                return layer;
            
            var current = Event.current;
            var changed = GUI.changed;
            
            var layers = _configuration.LayerNames.Where(x=> !string.IsNullOrEmpty(x)).ToArray();
            var selectedValueForControl = EditorGUIExtensions.GetSelectedValueForControl(controlId, -1);
            
            if (selectedValueForControl != -1)
            {
                if (selectedValueForControl >= layers.Length)
                {
                    EditorGUIUtility.PingObject(_configuration);
                    Selection.activeObject = _configuration;

                    GUI.changed = changed;
                }
                else
                {
                    var selected = 0;
                    for (var i = 0; i < LayerIdConfiguration.MaxLayerCount; i++)
                    {
                        if (string.IsNullOrEmpty(_configuration.GetLayerName(i)))
                            continue;

                        if (selected == selectedValueForControl)
                        {
                            layer = _configuration.GetLayerValue(i);
                            break;
                        }

                        selected++;
                    }
                }
            }

            if (current.type == EventType.MouseDown && position.Contains(current.mousePosition) || EditorGUIExtensions.MainActionKeyForControl(current, controlId))
            {
                var selected = 0;
                for (var i = 0; i < LayerIdConfiguration.MaxLayerCount; i++)
                {
                    if (!string.IsNullOrEmpty(_configuration.GetLayerName(i)))
                    {
                        if (_configuration.GetLayerValue(i) != layer)
                            selected++;
                        else
                            break;
                    }
                }

                
                ArrayUtility.Add(ref layers, "");
                ArrayUtility.Add(ref layers, "Add Layer...");

                EditorGUIExtensions.DoPopup(position, controlId, selected, EditorGUIExtensions.TempContent(layers), style);
                current.Use();

                return layer;
            }

            if (current.type == EventType.Repaint)
            {
                var content = EditorGUI.showMixedValue ? EditorGUIUtility.TrTextContent("—", "Mixed Values") : EditorGUIExtensions.TempContent(_configuration.GetLayerNameByValue(layer));
                style.Draw(position, content, controlId, false, position.Contains(current.mousePosition));
            }

            return layer;
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