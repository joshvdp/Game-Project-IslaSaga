using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Combat;
using Items.Weapon;
namespace Player.Combat
{
    public class PlayerAttackRangeDetect : MonoBehaviour
    {
        [SerializeField] PlayerCombat PlayerCombatCS;
        public List<GameObject> EnemiesInRange;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Enemy" && !other.isTrigger)
            {
                EnemiesInRange.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Enemy" && !other.isTrigger)
            {
                EnemiesInRange.Remove(other.gameObject);
            }
            
        }
    }
}