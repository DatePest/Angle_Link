using System;
using UnityEditor;
using UnityEngine;
namespace Editor_Tool
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]

    public class ConditionalHideAttribute : PropertyAttribute
    {
        public string BoolFieldName;

        public ConditionalHideAttribute(string bindingboolFieldName)
        {
            BoolFieldName = bindingboolFieldName;
        }
    }
    [CustomPropertyDrawer(typeof(ConditionalHideAttribute))]
    public class ConditionalHideDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // GetTarget
            var condHAtt = (ConditionalHideAttribute)attribute;
            SerializedProperty boolProp = property.serializedObject.FindProperty(condHAtt.BoolFieldName);

            if (boolProp != null && boolProp.boolValue)
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // GetTarget
            var condHAtt = (ConditionalHideAttribute)attribute;
            SerializedProperty boolProp = property.serializedObject.FindProperty(condHAtt.BoolFieldName);

            if (boolProp != null && boolProp.boolValue)
            {
                return EditorGUI.GetPropertyHeight(property, label, true);
            }
            return 0; // if ==0  HideTarget  
        }
    }
}

