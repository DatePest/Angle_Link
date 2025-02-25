using UnityEditor;
using UnityEngine;
using System.Reflection;
using System;
#if UNITY_EDITOR
namespace Editor_Tool
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ShowPropertyListAttribute : PropertyAttribute
    {
    }
    [CustomPropertyDrawer(typeof(ShowPropertyListAttribute), true)]
    public class ExpandablePropertyDrawer : PropertyDrawer
    {
        private bool foldout = true;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PropertyField(position, property, label);
            if (property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue != null)
            {

                GUIContent g = new(" ");
                foldout = EditorGUI.Foldout(
                    new Rect(-15, position.y, position.width, EditorGUIUtility.singleLineHeight),
                    foldout, g, true);

                if (foldout)
                {
                    // 繪製物件屬性
                    EditorGUI.indentLevel++;

                    Editor editor = Editor.CreateEditor(property.objectReferenceValue);
                    if (editor != null)
                    {
                        editor.OnInspectorGUI();
                    }

                    EditorGUI.indentLevel--;
                }
            }
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // 根據展開狀態調整高度
            if (foldout && property.objectReferenceValue != null)
            {
                return EditorGUIUtility.singleLineHeight * 1; // 預估高度，可根據屬性數量調整
            }
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
#endif