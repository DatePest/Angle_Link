using Editor_Tool;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Editor_Test.Test_Drawer;

public class Editor_Test : MonoBehaviour
{
    [LimitType(typeof(GameObject))]
    public GameObject TestGame;

    [Test]
    public string TestField;

     [HideInInspector]public List<ScriptableObject> List = new(3);

    public class TestAttribute: PropertyAttribute
    {

    }
    [CustomPropertyDrawer(typeof(TestAttribute))]
    public class Test_Drawer: PropertyDrawer
    {
        bool tob;
        Testenum testenum;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var Target = property.serializedObject.targetObject as Editor_Test;
            // EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginHorizontal();
            if (EditorGUILayout.LinkButton( "A"))
            {
                Debug.Log("A");
            }
            if (GUILayout.Button("B"))
            {
                Debug.Log("B");
            }
           
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(30);
            var t = (Testenum)EditorGUILayout.EnumPopup(testenum);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("LabelField");
            tob = EditorGUILayout.Toggle("Toggle!", tob);
            EditorGUILayout.EndHorizontal();
            // EditorGUI.ObjectField("Ta", property);

            for (int i = 0; i < Target.List.Count; i++)
            {
                EditorGUILayout.ObjectField($"List {i}", Target.List[i],typeof(ScriptableObject),true);
            }

            EditorGUILayout.BeginVertical();
            if (EditorGUILayout.DropdownButton(new GUIContent("DropdownButton"), FocusType.Keyboard))
            {
                var Table = new GenericMenu();
                Table.AddItem(new GUIContent("A"), false, () => debug("DropdownButton_A"));
                Table.AddItem(new GUIContent("B"), false, () => debug("DropdownButton_B"));
                Table.AddSeparator("");
                Table.AddItem(new GUIContent("C/C1"), false, () => debug("DropdownButton_C1"));
                Table.AddItem(new GUIContent("C/C2"), false, () => debug("DropdownButton_C2"));
                Table.DropDown(GUILayoutUtility.GetLastRect());
            }
            EditorGUILayout.EndVertical();
            EditorGUI.PropertyField(position, property, label, true);
            
            if(GUI.changed)
            {
                if(t != testenum)
                {
                    testenum = t;
                    Debug.Log(testenum);
                }
              
            }
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
        public void debug(string msg)
        {
            Debug.Log(msg);
        }

        public enum Testenum
        {
            a, b, c, d, e
        }
    }
}


