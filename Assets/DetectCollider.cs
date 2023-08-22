using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColliderScripts
{
    public class DetectCollider : MonoBehaviour
    {
        public bool ObjectWithinDetectRange;

        public GameObject ObjectThatIsInRange;

        private void OnTriggerStay(Collider other)
        {
            if (other == null) return;
            ObjectWithinDetectRange = true;
            ObjectThatIsInRange = other.gameObject;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other == null) return;
            ObjectWithinDetectRange = false;
            ObjectThatIsInRange = null;
        }
    }

}
