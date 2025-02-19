using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace RngDropTool
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EditorMenuAttribute: PropertyAttribute
    {
    }
    [CustomPropertyDrawer(typeof(EditorMenuAttribute))]
    public class EditorMenuAttributeDrawer : PropertyDrawer
    {
        RandomEnum MEnum;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.serializedObject.Update();
            if (property.managedReferenceValue != null)
                EditorGUI.PropertyField(position, property, label, true);
            var TempEnum = MEnum;
            MEnum = (RandomEnum)EditorGUILayout.EnumPopup(MEnum);
           // GUILayout.en

            if (MEnum != TempEnum)
            {
                //Random_EditorTool.PropertySwitch(property, MEnum);
                //EditorUtility.SetDirty(property.serializedObject.targetObject);
                //EditorGUILayout.EnumPopup(MEnum);
                //HandleUtility.Repaint();
                //EditorApplication.delayCall += () => DelayCall(property, MEnum);
                Debug.Log("B");
            }
            Debug.Log(MEnum);
        }

        public void DelayCall(SerializedProperty property, RandomEnum e)
        {
            Random_EditorTool.PropertySwitch(property, e);
           // MEnum = e;
            EditorUtility.SetDirty(property.serializedObject.targetObject);
            HandleUtility.Repaint();
            Debug.Log("DelayCall");
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

    
    }
}
