using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class SettingsHandler : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }
    }
}

