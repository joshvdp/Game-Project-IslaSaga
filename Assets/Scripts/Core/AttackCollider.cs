using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interface;
namespace ColliderScripts
{
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
    }
}

