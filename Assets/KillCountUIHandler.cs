using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Player;
namespace Manager
{
    public class KillCountUIHandler : MonoBehaviour
    {
        public TextMeshProUGUI KillCounterText;


        private void OnEnable()
        {
            GlobalEvents.Instance?.FindEvent("On Any Enemy Death")?.AddListener(UpdateKillCounterText);
        }
        private void OnDisable()
        {
            GlobalEvents.Instance?.FindEvent("On Any Enemy Death")?.RemoveListener(UpdateKillCounterText);
        }

        private void Start()
        {
            UpdateKillCounterText();
        }
        public void UpdateKillCounterText() => KillCounterText.text = MainManager.Instance.PlayerStatsSCO.PlayerKills.ToString();

       
        
    }
}

