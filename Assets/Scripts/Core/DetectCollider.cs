using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Collider))]
    public class DetectCollider : MonoBehaviour
    {
        public bool ObjectWithinDetectRange;

        [SerializeField] List<GameObject> ObjectsThatIsInRange; // For some reason, unity has an error if this is not seen in the inspector.

        private void OnTriggerEnter(Collider other)
        {
            if (other == null) return;
            ObjectWithinDetectRange = true;
            ObjectsThatIsInRange.Add(other.gameObject);
            UpdateList();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other == null) return;
            ObjectWithinDetectRange = false;
            ObjectsThatIsInRange.Remove(other.gameObject);
            UpdateList();
        }

        public void UpdateList()
        {
            for (int i = 0; i < ObjectsThatIsInRange.Count; i++)
            {
                if (ObjectsThatIsInRange[i] == null) ObjectsThatIsInRange.RemoveAt(i);
            }
        }

        public GameObject NearestGameobject()
        {
            UpdateList();
            GameObject NearestEnemy = null;
            float NearestEnemyDistance = Mathf.Infinity;
            for (int i = 0; i < ObjectsThatIsInRange.Count; i++)
            {
                if (ObjectsThatIsInRange[i] != null)
                {
                    if (Vector3.Distance(ObjectsThatIsInRange[i].transform.position, transform.position) < NearestEnemyDistance)
                    {
                        NearestEnemy = ObjectsThatIsInRange[i];
                    }
                }
            }
            return NearestEnemy;
        }

        public List<GameObject> AllGameobjectsInRange()
        {
            UpdateList();
            return ObjectsThatIsInRange;
        }
    }

}
