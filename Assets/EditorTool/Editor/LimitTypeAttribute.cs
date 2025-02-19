using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Editor_Tool
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class LimitTypeAttribute : PropertyAttribute
    {
        public Type AllowedType { get; private set; }

        public LimitTypeAttribute(Type allowedType)
        {
            AllowedType = allowedType;
        }
    }
    [CustomPropertyDrawer(typeof(LimitTypeAttribute))]
    public class LimitTypeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attribute = (LimitTypeAttribute)this.attribute;
            Type allowedType = attribute.AllowedType;


            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();
            var newValue = EditorGUI.ObjectField(position, label, property.objectReferenceValue, allowedType, false);
            var changed = EditorGUI.EndChangeCheck();
            if (changed)
            {
                property.objectReferenceValue = newValue;
            }
            EditorGUI.EndProperty();
        }
    }
}
