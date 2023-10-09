using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;
public class PlayerStatsUIHandler : MonoBehaviour
{
    [SerializeField] PlayerStats PlayerStatsSCO;
    [SerializeField] Slider PlayerHealthUI;

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
        PlayerHealthUI.value = PlayerStatsSCO.PlayerCurrentHealth;
    }
}
