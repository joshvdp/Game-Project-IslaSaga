using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InterfaceAndInheritables;
using Items;
public class Pickupable : MonoBehaviour
{
    public InventoryItem ItemData;
    private void OnTriggerEnter(Collider other)
    {
        other.transform.GetComponent<PlayerInventory>().AddItem(ItemData);
        Destroy(gameObject);
    }

}
