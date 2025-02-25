using System;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class ReadOnlyAttribute : PropertyAttribute
{
    
}

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;

        EditorGUI.PropertyField(position, property, label);

        GUI.enabled = true;
    }
}
#endif
