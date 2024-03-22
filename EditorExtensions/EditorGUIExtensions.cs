namespace Game.Code.EditorExtensions
{
    using System;
    using System.Reflection;
    using UnityEditor;
    using UnityEngine;

    public static class EditorGUIExtensions
    {
        private static readonly Type PopupCallbackInfoType = typeof(EditorGUI).GetNestedType("PopupCallbackInfo", BindingFlags.NonPublic);
        private static readonly MethodInfo GetSelectedValueForControlMethod = PopupCallbackInfoType.GetMethod("GetSelectedValueForControl", BindingFlags.Static | BindingFlags.Public, null,
            CallingConventions.Any,
            new[]
            {
                typeof(int),
                typeof(int)
            },
            null);

        private static readonly MethodInfo DoPopupMethod = typeof(EditorGUI).GetMethod("DoPopup", BindingFlags.Static | BindingFlags.NonPublic, null,
            CallingConventions.Any,
            new[]
            {
                typeof(Rect),
                typeof(int),
                typeof(int),
                typeof(GUIContent[]),
                typeof(GUIStyle)
            },
            null);

        private static readonly MethodInfo TempContentMethod = typeof(EditorGUIUtility).GetMethod("TempContent", BindingFlags.Static | BindingFlags.NonPublic, null,
            CallingConventions.Any,
            new[]
            {
                typeof(string[])
            },
            null);

        private static readonly MethodInfo TempContentMethod1 = typeof(EditorGUIUtility).GetMethod("TempContent", BindingFlags.Static | BindingFlags.NonPublic, null,
            CallingConventions.Any,
            new[]
            {
                typeof(string)
            },
            null);
        
        public static GUIContent TempContent(string text)
        {
            return (GUIContent) TempContentMethod1?.Invoke(null, new object[] {text});
        }
        
        public static GUIContent[] TempContent(string[] texts)
        {
            return (GUIContent[]) TempContentMethod?.Invoke(null, new object[] {texts});
        }

        public static int DoPopup(Rect position, int controlId, int selected, GUIContent[] popupValues, GUIStyle style)
        {
            return (int) DoPopupMethod.Invoke(null, new object[] {position, controlId, selected, popupValues, style});
        }
        
        public static int GetSelectedValueForControl(int controlId, int selected)
        {
            return (int) GetSelectedValueForControlMethod.Invoke(null, new object[] {controlId, selected});
        }
        
        public static bool MainActionKeyForControl(Event evt, int controlId)
        {
            if (GUIUtility.keyboardControl != controlId)
                return false;
            
            var flag = evt.alt || evt.shift || evt.command || evt.control;
            return evt.type == EventType.KeyDown && (evt.keyCode == KeyCode.Space || evt.keyCode == KeyCode.Return || evt.keyCode == KeyCode.KeypadEnter) && !flag;
        }
    }
}