using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class EnemyZone : MonoBehaviour
    {
        public UnityEvent onPlayerEnter;
        public UnityEvent onPlayerExit;
        
        private void OnTriggerEnter(Collider other) => onPlayerEnter?.Invoke();
        private void OnTriggerExit(Collider other) => onPlayerExit?.Invoke();
    }
}
