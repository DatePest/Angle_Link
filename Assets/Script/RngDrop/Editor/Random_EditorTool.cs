using UnityEditor;
using UnityEngine;

namespace RngDropTool
{
#if UNITY_EDITOR
    public class Random_EditorTool
    {
        public static void LayeredRandomButton(SerializedProperty property)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add WeightedRandom"))
            {
                AddLayered<WeightedRandom>(property);
            }
            if (GUILayout.Button("Add AllIndividuallyRandom"))
            {
                AddLayered<AllIndividuallyRandom>(property);
            }
            EditorGUILayout.EndHorizontal();
        }
        public static void EnsureManagedReferenceInitialized<T>(SerializedProperty property) where T : IRngRandom, new()
        {
            if (property.managedReferenceValue == null || !(property.managedReferenceValue is T))
            {
                property.managedReferenceValue = new T();
            }
        }

        public static void AddLayered<T>(SerializedProperty property) where T : IRngRandom, new()
        {
            var tar = new T();
            var L = property.managedReferenceValue as LayeredRandom;
            L.AddLayer(tar);
        }


        public static void PropertySwitch(SerializedProperty property, RandomEnum randomEnum)
        {
            property.serializedObject.Update();
            switch (randomEnum)
            {
                case RandomEnum.WeightedRandom:
                    EnsureManagedReferenceInitialized<WeightedRandom>(property);
                    break;
                case RandomEnum.LayeredRandom:
                    EnsureManagedReferenceInitialized<LayeredRandom>(property);
                    //LayeredRandomButton(property);
                    break;
                case RandomEnum.AllIndividuallyRandom:
                    EnsureManagedReferenceInitialized<AllIndividuallyRandom>(property);
                    break;
            }
            property.serializedObject.ApplyModifiedProperties();
        }
        
    }
#endif
}
