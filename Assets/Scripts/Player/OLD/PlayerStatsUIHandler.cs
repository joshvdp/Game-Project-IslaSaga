using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;
namespace Manager
{
    public class PlayerStatsUIHandler : MonoBehaviour
    {
        [SerializeField] PlayerStats PlayerStatsSCO;
        [SerializeField] Slider PlayerHealthUI;

        public bool CanUpdatePlayerStats = true;
        private void Awake()
        {
            PlayerHealthUI.maxValue = PlayerStatsSCO.PlayerMaxHealth;
            PlayerHealthUI.value = PlayerStatsSCO.PlayerCurrentHealth;
        }


        private void Update()
        {
            UpdateStats();
        }

        void UpdateStats()
        {
            if (CanUpdatePlayerStats) PlayerHealthUI.value = PlayerStatsSCO.PlayerCurrentHealth;
            if (MainManager.Instance.IsGameOver) CanUpdatePlayerStats = false; // This makes sure to update player stats one last time before game over
        }
    }
}

