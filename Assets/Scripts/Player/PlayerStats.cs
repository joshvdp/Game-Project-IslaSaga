using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "Player Stats", menuName = "Player/Stats")]
    public class PlayerStats : ScriptableObject
    {
        public float PlayerMaxHealth;
        public float PlayerCurrentHealth;

        public float PlayerSpeed;
        public float PlayerDamageMultiplier;

        


        public void TakeDamage(float Damage)
        {
            PlayerCurrentHealth -= Damage;
        }

        public void TakeHeal(float HealAmount)
        {

        }


    }
}

