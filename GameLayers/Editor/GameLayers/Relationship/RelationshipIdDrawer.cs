namespace Game.Code.Configuration.Editor.GameLayers.Relationship
{
    using System;
    using Code.GameLayers.Relationship;
    using EditorExtensions;
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(RelationshipId))]
    public sealed class RelationshipIdDrawer : PropertyDrawer
    {
        private static readonly int RelationshipIdHash = nameof(RelationshipIdHash).GetHashCode();
        
        private RelationshipId[] _configuration;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Initialize();

            EditorGUI.BeginProperty(position, label, property);

            var propertyValue = property.FindPropertyRelative("_value");
            propertyValue.intValue = RelationshipField(position, label, propertyValue.intValue, _configuration);
            property.serializedObject.ApplyModifiedProperties();

            EditorGUI.EndProperty();
        }

        public static int RelationshipField(Rect position, GUIContent label, int relationship, RelationshipId[] configuration)
        {
            return RelationshipField(position, label, relationship, EditorStyles.popup, configuration);
        }

        private static int RelationshipField(Rect position, GUIContent label, int relationship, GUIStyle style, RelationshipId[] configuration)
        {
            var controlId = GUIUtility.GetControlID(RelationshipIdHash, FocusType.Keyboard, position);
            position = EditorGUI.PrefixLabel(position, controlId, label);

            if (configuration == null)
                return relationship;
            
            var current = Event.current;
            var changed = GUI.changed;

            var layers = new string[configuration.Length];
            for (int i = 0; i < configuration.Length; i++)
            {
                layers[i] = configuration[i].ToString();
            }
            
            var selectedValueForControl = EditorGUIExtensions.GetSelectedValueForControl(controlId, -1);
            
            if (selectedValueForControl != -1)
            {
                if (selectedValueForControl >= configuration.Length)
                {
                    //EditorGUIUtility.PingObject(configuration);
                    //Selection.activeObject = configuration;

                    GUI.changed = changed;
                }
                else
                {
                    var selected = 0;
                    for (var i = 0; i < configuration.Length; i++)
                    {
                        if (selected == selectedValueForControl)
                        {
                            relationship = (int)configuration[i];
                            break;
                        }

                        selected++;
                    }
                }
            }

            if (current.type == EventType.MouseDown && position.Contains(current.mousePosition) || EditorGUIExtensions.MainActionKeyForControl(current, controlId))
            {
                var selected = 0;
                for (var i = 0; i < configuration.Length; i++)
                {
                    if ((int)configuration[i] != relationship)
                        selected++;
                    else
                        break;
                }

                
                ArrayUtility.Add(ref layers, "");
                ArrayUtility.Add(ref layers, "Add new");

                EditorGUIExtensions.DoPopup(position, controlId, selected, EditorGUIExtensions.TempContent(layers), style);
                current.Use();

                return relationship;
            }

            if (current.type == EventType.Repaint)
            {
                var content = EditorGUI.showMixedValue 
                    ? EditorGUIUtility.TrTextContent("—", "Mixed Values") 
                    : EditorGUIExtensions.TempContent(((RelationshipId)relationship).ToString());
                style.Draw(position, content, controlId, false, position.Contains(current.mousePosition));
            }

            return relationship;
        }

        private void Initialize()
        {
            _configuration = Enum.GetValues(typeof(RelationshipId)) as RelationshipId[];
        }
    }
}