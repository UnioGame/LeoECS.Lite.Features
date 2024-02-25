namespace Game.Code.Timeline.Shared
{
    using UnityEditor;
    using UnityEngine;

    internal interface IAttributePropertyDrawer
    {
#if UNITY_EDITOR
        void OnGUI(Rect position, SerializedProperty property, GUIContent label);

        float GetPropertyHeight(SerializedProperty property, GUIContent label);
#endif
    }
}