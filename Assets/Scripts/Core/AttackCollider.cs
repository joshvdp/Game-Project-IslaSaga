using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InterfaceAndInheritables;
using UnityEngine.Events;

namespace Core
{
    [RequireComponent(typeof(Collider))]
    public class AttackCollider : MonoBehaviour
    {
        public UnityEvent OnTargetHit;
        public UnityEvent OnNoTargetHit;
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
                    ObjectsToDamage.Remove(ObjectsToDamage[i]);
                }
            }
            if (ObjectsToDamage.Count <= 0) ObjectWithinAttackRange = false;
        }

        public void AttackAllTargets(float damage, DamageType DamageType)
        {
            UpdateList();
            for (int i = 0; i < ObjectsToDamage.Count; i++)
            {
                IDamageable target = ObjectsToDamage[i].GetComponent<IDamageable>();
                if (target != null)
                {
                    target.Hit(damage, DamageType.MELEE);
                    OnTargetHit?.Invoke();
                }
                else Debug.Log("IDAMAGEABLE IS MISSING ON " + ObjectsToDamage[i]);
            }
            if (!ObjectWithinAttackRange) OnNoTargetHit?.Invoke();
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

            if (NearestEnemy != null)
            {
                NearestEnemy.Hit(damage, DamageType);
                OnTargetHit?.Invoke();
            }
            else OnNoTargetHit?.Invoke();
        }
    }
}

