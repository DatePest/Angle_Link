using Editor_Tool;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
namespace RngDropTool
{
   
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class RandomBindEnumAttribute : PropertyAttribute
    {
        public string FieldName;
        public RandomBindEnumAttribute(string fieldName)
        {
            FieldName = fieldName;
        }
    }
    [CustomPropertyDrawer(typeof(RandomBindEnumAttribute))]
    public class RandomBindEnumDrawer : PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label, true);
            var attribute = this.attribute as RandomBindEnumAttribute;

            var field = property.serializedObject.targetObject.GetType().GetField(attribute.FieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (field == null)
            {
                Debug.LogError($"FieldName Not Found"); return;
            }
            if (field.FieldType != typeof(RandomEnum))
            {
                Debug.LogError($"FieldName Not type RandomEnum "); return;
            }

            var myEnumProperty = property.serializedObject.FindProperty(attribute.FieldName);

            switch (myEnumProperty.enumValueIndex)
            {
                case (int)RandomEnum.WeightedRandom:
                    Random_EditorTool.EnsureManagedReferenceInitialized<WeightedRandom>(property);
                    break;
                case (int)RandomEnum.LayeredRandom:
                                Random_EditorTool.EnsureManagedReferenceInitialized<LayeredRandom>(property);
                    Random_EditorTool.LayeredRandomButton(property);
                    break;
                case (int)RandomEnum.AllIndividuallyRandom:
                    Random_EditorTool.EnsureManagedReferenceInitialized<AllIndividuallyRandom>(property);
                    break;

            }
            AutoAllWeight(property.managedReferenceValue as IRngRandom);
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
       
        void AutoAllWeight(IRngRandom random)
        {
            if (random == null) return; 
            random.CalculateWeight();
        }

        
    }
}
