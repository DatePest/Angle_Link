using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbilityData))]
public class AbilityDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();
        AbilityData data = (AbilityData)target;
        bool Added = false;
        if (data.effects != null )
        {
          
            foreach (var d in data.effects)
            {
                if (!Added && d.effect is EffectAddStatu)
                {
                    var p = this.serializedObject.FindProperty("StatuDurationFormula");
                    EditorGUILayout.PropertyField(p);
                    Added = true;
                }
                if(d.CalculateFormula == string.Empty)
                {
                    d.CalculateFormula = "100 + {Lv} * 10 " ;
                }
            }
        }
       
        if(!Added) { data.StatuDurationFormula = string.Empty; }
        serializedObject.ApplyModifiedProperties();
       
        if (GUI.changed)
        {
            EditorUtility.SetDirty(data);
        }
    }
  
}