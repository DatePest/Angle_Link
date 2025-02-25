using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RngDropTool
{
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(IRngRandom), true)]
    public class IRngRandomDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            string typeName = property.managedReferenceFullTypename;

            string shortTypeName = string.IsNullOrEmpty(typeName)
                ? "NullType"
                : typeName.Substring(typeName.LastIndexOf('.') + 1);

            Rect labelRect = new Rect(position.x + 15, position.y, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(labelRect, $"{label.text} - {shortTypeName}");

            //Rect fieldRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, position.height - EditorGUIUtility.singleLineHeight - 2);
            EditorGUI.PropertyField(position, property, GUIContent.none, true);

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float extraHeight = EditorGUI.GetPropertyHeight(property, true);
            return EditorGUIUtility.singleLineHeight + 2 + extraHeight;
        }
    }
#endif
}
