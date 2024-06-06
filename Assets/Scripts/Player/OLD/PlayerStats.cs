using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using System;

namespace Player
{
    [CreateAssetMenu(fileName = "Player Stats", menuName = "Player/Stats")]
    public class PlayerStats : ScriptableObject
    {
        public Action OnChangeHp;
        public Action OnHpPotionUsed;
        public float PlayerMaxHealth;
        public float PlayerCurrentHealth;

        public float PlayerSpeed;
        public float PlayerDamageMultiplier;

        public int PlayerKills = 0;
        public bool IsInvincible = false;

        const float DEFAULT_MAX_HP = 100f;

        public void TakeDamage(float Damage)
        {
            if (IsInvincible) return;
            PlayerCurrentHealth = Mathf.Clamp(PlayerCurrentHealth - Damage, 0, PlayerMaxHealth);
            OnChangeHp?.Invoke();
        }

        public void TakeHeal(float HealAmount)
        {
            float FinalHealAmount = HealAmount;
            PlayerCurrentHealth = Mathf.Clamp(PlayerCurrentHealth + HealAmount, 0, PlayerMaxHealth);
            OnChangeHp?.Invoke();
        }
        public void IncreaseMaxHP(float amount)
        {
            PlayerMaxHealth += amount;
            TakeHeal(amount);
            OnChangeHp?.Invoke();
        }
        public void Reset()
        {
            PlayerMaxHealth = DEFAULT_MAX_HP;
            PlayerCurrentHealth = PlayerMaxHealth;
            PlayerKills = 0;
        }

        public void AddPlayerKill() => PlayerKills++;
    }
}

