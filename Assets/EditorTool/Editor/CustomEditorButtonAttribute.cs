using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
namespace Editor_Tool
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]

    public class CustomEditorButtonAttribute : PropertyAttribute
    {
        public string MethodName;
        public CustomEditorButtonAttribute(string methodName)
        {
            this.MethodName = methodName;
        }
    }

    [CustomPropertyDrawer(typeof(CustomEditorButtonAttribute))]
    public class ShowInitializeButtonDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attribute_ = (CustomEditorButtonAttribute)attribute;
            string methodName = attribute_.MethodName;
            if (GUILayout.Button(methodName))
            {
               
                if (!string.IsNullOrEmpty(methodName))
                { 
                    var TargetScript = property.serializedObject.targetObject;
                    var methodInfo = TargetScript.GetType().GetMethod(methodName);
                    if (methodInfo != null)
                    {
                        methodInfo.Invoke(TargetScript, null); 
                    }
                    else
                    {
                        Debug.LogError($"Method {methodName} not found!");
                    }
                }
            }
            EditorGUI.PropertyField(position, property, label);
            position.y += EditorGUI.GetPropertyHeight(property, true) + EditorGUIUtility.standardVerticalSpacing;
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
    }
}