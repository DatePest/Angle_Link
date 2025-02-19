using Codice.Client.BaseCommands;
using Editor_Tool;
using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using static Editor_Tool.ShowType.Base;
using static Editor_Tool.ShowType;
namespace Editor_Tool
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]

    public class ShowPropertyAttribute : PropertyAttribute
    {
        public string BoolFieldName;
        public ShowPropertyAttribute(string boolFieldName)
        {
            BoolFieldName = boolFieldName;
        }
    }
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]

    public class ShowProperty_CanFoldAttribute : ShowPropertyAttribute
    {
        public ShowProperty_CanFoldAttribute(string boolFieldName) : base(boolFieldName)
        {
        }
    }

    [CustomPropertyDrawer(typeof(ShowProperty_CanFoldAttribute))]
    public class ShowProperty_CanFold : PropertyDrawer
    {
        const int Xoffset = 25;
        private bool isFoldout = true;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!Base.OnGUI(ref position, property, label, (ShowPropertyAttribute)attribute, out var targetproperty)) return;
            ShowT2.OnGUI(ref position, targetproperty, ref isFoldout, Xoffset);
            return;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var H = Base.GetPropertyHeight(property, label, (ShowPropertyAttribute)attribute, out var targetproperty);
            return ShowT2.GetPropertyHeight(targetproperty, H, isFoldout);
        }
    }



    [CustomPropertyDrawer(typeof(ShowPropertyAttribute))]
    public class ShowProperty : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!Base.OnGUI(ref position, property, label, (ShowPropertyAttribute)attribute, out var targetproperty)) return;
            ShowT1.OnGUI(targetproperty);
            return;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var H = Base.GetPropertyHeight(property, label, (ShowPropertyAttribute)attribute, out var targetproperty);
            return ShowT1.GetPropertyHeight(targetproperty, H);
        }
    }
    public class ShowType
    {
        public class Base
        {
            public static bool OnGUI(ref Rect position, SerializedProperty property, GUIContent label, ShowPropertyAttribute attribute, out SerializedProperty TargetScriptProp)
            {
                TargetScriptProp = null;
                var boolProp = property.serializedObject.FindProperty(attribute.BoolFieldName);

                // 繪製當前屬性
                EditorGUI.PropertyField(position, property, label, true);
                position.y += EditorGUI.GetPropertyHeight(property, true) + EditorGUIUtility.standardVerticalSpacing;

                // 如果控制條件為 false，直接返回
                if (boolProp == null || !boolProp.boolValue)
                    return false;

                // 獲取目標屬性
                TargetScriptProp = property.serializedObject.FindProperty(property.name);
                if (TargetScriptProp == null || TargetScriptProp.objectReferenceValue == null)
                    return false;

                return true;
            }
            public static float GetPropertyHeight(SerializedProperty property, GUIContent label, ShowPropertyAttribute attribute, out SerializedProperty TargetScriptProp)
            {
                TargetScriptProp = null;
                var boolProp = property.serializedObject.FindProperty(attribute.BoolFieldName);

                float totalHeight = EditorGUI.GetPropertyHeight(property, label, true); // 當前屬性的高度
                if (boolProp == null || !boolProp.boolValue)
                    return totalHeight;

                // 獲取引用對象的屬性高度
                TargetScriptProp = property.serializedObject.FindProperty(property.name);

                return totalHeight;
            }
            public class ShowT1
            {
                /// <summary>
                /// ---- Properties: -----
                /// A
                /// B
                /// C
                /// ---- End -----
                /// </summary>
                /// <param name="TargetPro"></param>
                public static void OnGUI(SerializedProperty TargetPro)
                {
                    if (TargetPro != null && TargetPro.objectReferenceValue != null)
                    {
                        var bScriptSerializedObject = new SerializedObject(TargetPro.objectReferenceValue);
                        bScriptSerializedObject.Update();

                        var bScriptSerializedProperty = bScriptSerializedObject.GetIterator();
                        var content = new GUIContent("---- Properties: -----");
                        EditorGUILayout.LabelField(content);
                        while (bScriptSerializedProperty.NextVisible(true))
                        {
                            // 跳過不需要顯示的屬性（例如 m_Script）
                            if (bScriptSerializedProperty.name == "m_Script")
                                continue;

                            EditorGUILayout.PropertyField(bScriptSerializedProperty, true);

                        }
                        EditorGUILayout.LabelField(new GUIContent("---- End -----"));
                        bScriptSerializedObject.ApplyModifiedProperties();
                    }
                }
                public static float GetPropertyHeight(SerializedProperty property, float CurrentHeight)
                {
                    if (property == null || property.serializedObject == null || property.objectReferenceValue == null) return CurrentHeight;

                    var bScriptSerializedObject = new SerializedObject(property.objectReferenceValue);
                    bScriptSerializedObject.Update();

                    var bScriptSerializedProperty = bScriptSerializedObject.GetIterator();
                    bScriptSerializedProperty.NextVisible(true);

                    //CurrentHeight += EditorGUIUtility.standardVerticalSpacing;
                    //while (bScriptSerializedProperty.NextVisible(false))
                    //{
                    //    if (bScriptSerializedProperty.name == "m_Script")
                    //        continue;

                    //    CurrentHeight += EditorGUI.GetPropertyHeight(bScriptSerializedProperty, true) + EditorGUIUtility.standardVerticalSpacing;
                    //}
                    bScriptSerializedObject.ApplyModifiedProperties();

                    return CurrentHeight;
                }


            }
            public class ShowT2
            {
                /// <summary> CanFoldout
                /// X---- Properties: -----
                ///     A
                ///     B
                ///     C
                /// ---- End -----
                /// </summary>
                public static void OnGUI(ref Rect position, SerializedProperty TargetPro, ref bool isFoldout, int Xoffset)
                {
                    // 顯示 Properties 折疊框
                    Rect foldoutRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
                    isFoldout = EditorGUI.Foldout(foldoutRect, isFoldout, "Properties", true);
                    position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                    if (isFoldout)
                    {
                        var targetSerializedObject = new SerializedObject(TargetPro.objectReferenceValue);
                        targetSerializedObject.Update();

                        var iterator = targetSerializedObject.GetIterator();
                        iterator.NextVisible(true); // 定位到第一個屬性

                        while (iterator.NextVisible(false))
                        {
                            // 過濾掉 m_Script
                            if (iterator.name == "m_Script")
                                continue;

                            // 繪製屬性
                            float propertyHeight = EditorGUI.GetPropertyHeight(iterator, true);
                            Rect propertyRect = new Rect(position.x + Xoffset, position.y, position.width - Xoffset, propertyHeight);
                            EditorGUI.PropertyField(propertyRect, iterator, true);

                            position.y += propertyHeight + EditorGUIUtility.standardVerticalSpacing;
                        }

                        targetSerializedObject.ApplyModifiedProperties();
                    }
                }

                public static float GetPropertyHeight(SerializedProperty property, float CurrentHeight, bool isFoldout)
                {
                    if (property == null || property.serializedObject == null || property.objectReferenceValue == null) return CurrentHeight;

                    CurrentHeight += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

                    if (isFoldout)
                    {
                        var targetSerializedObject = new SerializedObject(property.objectReferenceValue);
                        targetSerializedObject.Update();

                        var iterator = targetSerializedObject.GetIterator();
                        iterator.NextVisible(true);

                        while (iterator.NextVisible(false))
                        {
                            if (iterator.name == "m_Script")
                                continue;

                            CurrentHeight += EditorGUI.GetPropertyHeight(iterator, true) + EditorGUIUtility.standardVerticalSpacing;
                        }
                    }

                    return CurrentHeight;
                }
            }
        }


    }
}