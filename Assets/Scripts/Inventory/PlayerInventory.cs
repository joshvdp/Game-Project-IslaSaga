using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] UnityEvent ItemAdded;
    [SerializeField] UnityEvent ItemRemoved;

    public List<InventoryItem> InventoryItems;

    public void AddItem(InventoryItem Item)
    {
        InventoryItems.Add(Item);
        InventoryUIHandler.Instance.AddItemToUI(Item);
        ItemAdded?.Invoke();
    }

    public void RemoveItem(InventoryItem Item, string slotID)
    {
        InventoryItems.Remove(Item);
        InventoryUIHandler.Instance.RemoveItemFromUI(Item, slotID);
        ItemRemoved?.Invoke();
    }
}
