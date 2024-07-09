using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EconomyTrigger : MonoBehaviour
{
    public EconomyModifier modifier;
    public void IncreaseCriminalityValue()
    {
        modifier.UpdateCriminalityValues(0.4f);
    }
    public void ReduceCriminalityValue()
    {
        modifier.UpdateCriminalityValues(-0.4f);
    }   

    public void ClimateModifier(float i)
    {
        modifier.UpdateClimateValues(i);
    }
    public void RecoursesAbundanceModifier(int i)
    {
        modifier.UpdateRecoursesAbundanceValues(-i);
    }
}

[CustomEditor(typeof(EconomyTrigger))]
class EconomyTriggerInspectorEditor : Editor {
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EconomyTrigger economyManager = (EconomyTrigger)target;

        if (GUILayout.Button("Aconteceu roubo"))
        {
            economyManager.IncreaseCriminalityValue();
        }
        if (GUILayout.Button("Ladrão preso"))
        {
            economyManager.ReduceCriminalityValue();
        }

        float climateIndex = 0;
        climateIndex = EditorGUILayout.Slider("Change General values", climateIndex, -10, 10);
        if (GUILayout.Button("Mudança de clima"))
        {
            economyManager.ClimateModifier(climateIndex);
        }
        float abundanceIndex = 0;
        abundanceIndex = EditorGUILayout.Slider("Change General values", abundanceIndex, -10, 10);
        if (GUILayout.Button("Arvores derrubadas"))
        {
            economyManager.ClimateModifier(abundanceIndex);
        }



        if (GUI.changed)
        {
            EditorUtility.SetDirty(economyManager);
        }
    }
}
