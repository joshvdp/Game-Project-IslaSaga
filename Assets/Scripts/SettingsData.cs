using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings Data", menuName = "Settings Data")]
public class SettingsData : ScriptableObject
{
    public float LookSensitivityValue;

    public void ChangeLookSensitivityValue(float LookSensitivity) => LookSensitivityValue = LookSensitivity;
}
