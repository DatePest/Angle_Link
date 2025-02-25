using UnityEditor;
using UnityEngine;

namespace RngDropTool
{
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(RngDrop))]
    public class RngDropDrawer : PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label, true);

            var RandomType = property.FindPropertyRelative("RandomType");

            var IRngRandom = property.FindPropertyRelative("items");

            switch (RandomType.enumValueIndex)
            {
                case (int)RandomEnum.WeightedRandom:
                    Random_EditorTool.EnsureManagedReferenceInitialized<WeightedRandom>(IRngRandom);
                    break;
                case (int)RandomEnum.LayeredRandom:
                    Random_EditorTool.EnsureManagedReferenceInitialized<LayeredRandom>(IRngRandom);
                    Random_EditorTool.LayeredRandomButton(IRngRandom);
                    break;
                case (int)RandomEnum.AllIndividuallyRandom:
                        Random_EditorTool.EnsureManagedReferenceInitialized<AllIndividuallyRandom>(IRngRandom);
                    break;

            }
            AutoAllWeight(IRngRandom.managedReferenceValue as IRngRandom);
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
#endif
}
