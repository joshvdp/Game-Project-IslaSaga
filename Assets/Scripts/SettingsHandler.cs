using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Manager
{
    public class SettingsHandler : MonoBehaviour
    {
        public static SettingsHandler Instance;
        public SettingsData settingsData;
        
        [SerializeField] TMP_Dropdown QualitySettingsDropdown;
        [SerializeField] Slider LookSensitivitySlider;
        private void Awake()
        {
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this;
            Application.targetFrameRate = 60;
            SetSettings();


        }
        public void SetSettings()
        {
            SetQualitySettings((int)settingsData.GraphicsQualityValue);
            UpdateLookSensSliderValue(settingsData.LookSensitivityValue);
        }
        public void UpdateLookSensSliderValue(float value)
        {
            if (LookSensitivitySlider == null) return;
            LookSensitivitySlider.value = value;
        }
        public void SetQualitySettings(int index)
        {
            QualitySettings.SetQualityLevel(index);
            Debug.Log("Set Qualit level to " + index);
            settingsData.ChangeQualitySettingsValue(index);
        }


    }

    public enum GraphicsQuality
    {
        LOW = 0,
        MEDIUM = 1,
        HIGH = 2
    }
}

