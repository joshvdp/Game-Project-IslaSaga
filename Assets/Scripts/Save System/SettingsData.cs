using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using Player.Controls;
[CreateAssetMenu(fileName = "Settings Data", menuName = "Settings Data")]
public class SettingsData : ScriptableObject
{
    public float LookSensitivityValue = 0.5f;
    public int TargetFPS = 144;
    public GraphicsQuality GraphicsQualityValue = GraphicsQuality.MEDIUM;
    public PlatformType PlatformType;
    public void ChangeLookSensitivityValue(float LookSensitivity) => LookSensitivityValue = LookSensitivity;
    public void ChangeQualitySettingsValue(int index) => GraphicsQualityValue = (GraphicsQuality)index;
    public void SetQualitySettings(int index)
    {
        QualitySettings.SetQualityLevel(index);
        Debug.Log("Set Qualit level to " + index);
        ChangeQualitySettingsValue(index);
    }


}
