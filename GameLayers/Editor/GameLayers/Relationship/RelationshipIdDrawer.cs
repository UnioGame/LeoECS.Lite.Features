namespace Game.Code.Configuration.Editor.GameLayers.Relationship
{
    using System.Linq;
    using Code.GameLayers.Relationship;
    using EditorExtensions;
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(RelationshipId))]
    public sealed class RelationshipIdDrawer : PropertyDrawer
    {
        private static readonly int RelationshipIdHash = nameof(RelationshipIdHash).GetHashCode();
        
        private RelationshipIdConfiguration _configuration;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Initialize();

            EditorGUI.BeginProperty(position, label, property);

            var propertyValue = property.FindPropertyRelative("_value");
            propertyValue.intValue = RelationshipField(position, label, propertyValue.intValue, _configuration);
            property.serializedObject.ApplyModifiedProperties();

            EditorGUI.EndProperty();
        }

        public static int RelationshipField(Rect position, GUIContent label, int relationship, RelationshipIdConfiguration configuration)
        {
            return RelationshipField(position, label, relationship, EditorStyles.popup, configuration);
        }

        private static int RelationshipField(Rect position, GUIContent label, int relationship, GUIStyle style, RelationshipIdConfiguration configuration)
        {
            var controlId = GUIUtility.GetControlID(RelationshipIdHash, FocusType.Keyboard, position);
            position = EditorGUI.PrefixLabel(position, controlId, label);

            if (configuration == null)
                return relationship;
            
            var current = Event.current;
            var changed = GUI.changed;
            
            var layers = configuration.Relationships.Where(x=> !string.IsNullOrEmpty(x)).ToArray();
            var selectedValueForControl = EditorGUIExtensions.GetSelectedValueForControl(controlId, -1);
            
            if (selectedValueForControl != -1)
            {
                if (selectedValueForControl >= layers.Length)
                {
                    EditorGUIUtility.PingObject(configuration);
                    Selection.activeObject = configuration;

                    GUI.changed = changed;
                }
                else
                {
                    var selected = 0;
                    for (var i = 0; i < RelationshipIdConfiguration.MaxRelationshipsCount; i++)
                    {
                        if (string.IsNullOrEmpty(configuration.GetRelationshipName(i)))
                            continue;

                        if (selected == selectedValueForControl)
                        {
                            relationship = configuration.GetRelationshipValue(i);
                            break;
                        }

                        selected++;
                    }
                }
            }

            if (current.type == EventType.MouseDown && position.Contains(current.mousePosition) || EditorGUIExtensions.MainActionKeyForControl(current, controlId))
            {
                var selected = 0;
                for (var i = 0; i < RelationshipIdConfiguration.MaxRelationshipsCount; i++)
                {
                    if (!string.IsNullOrEmpty(configuration.GetRelationshipName(i)))
                    {
                        if (configuration.GetRelationshipValue(i) != relationship)
                            selected++;
                        else
                            break;
                    }
                }

                
                ArrayUtility.Add(ref layers, "");
                ArrayUtility.Add(ref layers, "Add Relationship...");

                EditorGUIExtensions.DoPopup(position, controlId, selected, EditorGUIExtensions.TempContent(layers), style);
                current.Use();

                return relationship;
            }

            if (current.type == EventType.Repaint)
            {
                var content = EditorGUI.showMixedValue ? EditorGUIUtility.TrTextContent("—", "Mixed Values") : EditorGUIExtensions.TempContent(configuration.GetRelationshipNameByValue(relationship));
                style.Draw(position, content, controlId, false, position.Contains(current.mousePosition));
            }

            return relationship;
        }

        private void Initialize()
        {
            if (_configuration != null)
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