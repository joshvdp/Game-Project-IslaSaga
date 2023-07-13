using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interface;
using Player;
public class PlayerHpHandler : MonoBehaviour, IDamageable
{
    [SerializeField] PlayerStats PlayerStatsCS;
    public float MaxHealth { get { return PlayerStatsCS.PlayerMaxHealth;} set { PlayerStatsCS.PlayerCurrentHealth = value; } }
    public float CurrentHealth { get { return PlayerStatsCS.PlayerCurrentHealth; } set { PlayerStatsCS.PlayerCurrentHealth = value; } }
    private void Start()
    {

    }
    public void Hit(float Damage)
    {
        PlayerStatsCS.TakeDamage(Damage);
        CurrentHealth = PlayerStatsCS.PlayerCurrentHealth;
        CheckHealth();
    }

    void CheckHealth()
    {
        if (CurrentHealth <= 0)
        {
            Death();
        }
    }
    public void Death()
    {
        Debug.Log("Player Dead");
    }
}
