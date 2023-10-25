using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InterfaceAndInheritables;
namespace Core
{
    [RequireComponent(typeof(Collider))]
    public class AttackCollider : MonoBehaviour
    {
        public List<GameObject> ObjectsToDamage; // Must be gameobject so we can see the detected object in the inspector.
                                                 // For some reason, unity doesn't support seeing interfaces in inspector.
        public bool ObjectWithinAttackRange;
        private void OnTriggerEnter(Collider other)
        {
            if (other == null) return;
            ObjectWithinAttackRange = true;
            ObjectsToDamage.Add(other.gameObject);
            UpdateList();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other == null) return;
            ObjectWithinAttackRange = false;
            ObjectsToDamage.Remove(other.gameObject);
            UpdateList();
        }

        public void UpdateList()
        {
            for (int i = 0; i < ObjectsToDamage.Count; i++)
            {
                if (ObjectsToDamage[i] == null)
                {
                    ObjectsToDamage.RemoveAt(i);
                }
            }
        }

        public void AttackAllTargets(float damage, DamageType DamageType)
        {
            UpdateList();
            for (int i = 0; i < ObjectsToDamage.Count; i++)
            {
                if (ObjectsToDamage[i] != null)
                    ObjectsToDamage[i].GetComponent<IDamageable>().Hit(damage, DamageType.MELEE);

                else Debug.Log("IDAMAGEABLE IS MISSING ON " + ObjectsToDamage[i]);
            }
        }

        public void AttackNearestTarget(float damage, DamageType DamageType)
        {
            UpdateList();
            IDamageable NearestEnemy = null;
            float NearestEnemyDistance = Mathf.Infinity;
            for (int i = 0; i < ObjectsToDamage.Count; i++)
            {
                if (ObjectsToDamage[i].GetComponent<IDamageable>() != null)
                {
                    if(Vector3.Distance(ObjectsToDamage[i].transform.position, transform.position) < NearestEnemyDistance)
                    {
                        NearestEnemy = ObjectsToDamage[i].GetComponent<IDamageable>();
                    }
                }
                else Debug.Log("IDAMAGEABLE IS MISSING ON " + ObjectsToDamage[i]);
            }

            if(NearestEnemy != null) NearestEnemy.Hit(damage, DamageType);
        }
    }
}

