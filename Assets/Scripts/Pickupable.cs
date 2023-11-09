using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InterfaceAndInheritables;
using Items;
using UnityEngine.Events;

namespace Items
{
    [RequireComponent(typeof(Collider))]
    public class Pickupable : MonoBehaviour
    {
        public UnityEvent OnPickup;

        public InventoryItem ItemData;
        private void OnTriggerEnter(Collider other)
        {
            
            if(!other.transform.GetComponent<PlayerInventory>().PickUpItem(ItemData)) return;
            OnPickup?.Invoke();
            Destroy(gameObject);
        }

    }
}

