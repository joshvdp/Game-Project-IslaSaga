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
        public float PlayerMaxHealth;
        public float PlayerCurrentHealth;

        public float PlayerSpeed;
        public float PlayerDamageMultiplier;



        public void TakeDamage(float Damage)
        {
            PlayerCurrentHealth = Mathf.Clamp(PlayerCurrentHealth - Damage, 0, PlayerMaxHealth);
            OnChangeHp?.Invoke();
        }

        public void TakeHeal(float HealAmount)
        {
            float FinalHealAmount = HealAmount;
            PlayerCurrentHealth = Mathf.Clamp(PlayerCurrentHealth + HealAmount, 0, PlayerMaxHealth);
            OnChangeHp?.Invoke();
        }

        public void Reset()
        {
            PlayerCurrentHealth = PlayerMaxHealth;
        }
    }
}

