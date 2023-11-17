using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class SettingsHandler : MonoBehaviour
    {
        public static SettingsHandler Instance;

        public SettingsData settingsData;
        private void Awake()
        {
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this;
            Application.targetFrameRate = 60;
        }
    }
}

