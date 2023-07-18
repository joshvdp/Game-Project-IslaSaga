using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interface;
using Player;
using Manager;
public class PlayerHpHandler : MonoBehaviour, IDamageable
{
    public delegate void OnPlayerDeath();
    public OnPlayerDeath onPlayerDeath;

    [SerializeField] PlayerReferences References;
    public float MaxHealth { get { return References.PlayerStatsCS.PlayerMaxHealth;} set { References.PlayerStatsCS.PlayerCurrentHealth = value; } }
    public float CurrentHealth { get { return References.PlayerStatsCS.PlayerCurrentHealth; } set { References.PlayerStatsCS.PlayerCurrentHealth = value; } }
    public bool IsAlive;

    private void Awake()
    {
        References.PlayerStatsCS.Reset();
    }
    public void Hit(float Damage)
    {
        References.PlayerStatsCS.TakeDamage(Damage);
        CurrentHealth = References.PlayerStatsCS.PlayerCurrentHealth;
        CheckHealth();
    }

    void CheckHealth()
    {
        if (CurrentHealth <= 0 && IsAlive)
        {
            Death();
        }
    }
    public void Death()
    {
        
        References.PlayerAnimCS.PlayerAnimator.SetBool("IsDead", true);
        References.PlayerAnimCS.PlayerAnimator.SetTrigger("Death");
        onPlayerDeath?.Invoke();
        IsAlive = false;
    }

}
