using System;
using UnityEditor;
using UnityEngine;

public class Test2Attribute : PropertyAttribute
{
    public Type AllowedType { get; private set; }
    public Test2Attribute(Type AllowedType)
    {
        this.AllowedType = AllowedType;
    }

}
[CustomPropertyDrawer(typeof(Test2Attribute))]
public class TestAtt2_Drawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        var att = attribute as Test2Attribute;
        if (att.AllowedType.IsAssignableFrom(typeof(int)))
        {

          
        }

        var newValue = EditorGUI.ObjectField(position, label, property.objectReferenceValue, typeof(UnityEngine.Object), false);
     
       

        EditorGUI.EndProperty();
    }
}