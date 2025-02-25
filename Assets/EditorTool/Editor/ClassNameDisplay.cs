using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
namespace EditorTool
{

    public class SerializeReference_ClassNameDisplayAttribute : PropertyAttribute { }
    [CustomPropertyDrawer(typeof(SerializeReference_ClassNameDisplayAttribute))]
    public class SerializeReference_ClassNameDisplayAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if(property.managedReferenceValue != null)
            {
                string className = property.managedReferenceValue.GetType().Name;
                // EditorGUI.LabelField(position, label.text, className);

                EditorGUI.LabelField(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), label.text, className);
                position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            }
           
            EditorGUI.PropertyField(position, property, GUIContent.none, true);

        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = EditorGUI.GetPropertyHeight(property, label, true);
            return height + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        }
    }
}
#endif