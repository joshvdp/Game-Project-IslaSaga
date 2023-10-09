using System.Collections.Generic;
using UnityEngine;
namespace Player.Combat
{
    public class PlayerAttackRangeDetect : MonoBehaviour
    {
        public List<GameObject> EnemiesInRange;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Enemy" && !other.isTrigger)
            {
                EnemiesInRange.Add(other.gameObject);
                UpdateList();
                
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Enemy" && !other.isTrigger)
            {
                EnemiesInRange.Remove(other.gameObject);
                UpdateList();
            }
            
        }

        public void UpdateList()
        {
            for (int i = 0; i < EnemiesInRange.Count; i++)
            {
                if (EnemiesInRange[i].gameObject == null)
                {
                    EnemiesInRange.RemoveAt(i);
                }
            }
        }
    }
}