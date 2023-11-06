using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] UnityEvent OnItemAdded;
    [SerializeField] UnityEvent OnItemRemoved;

    public List<InventoryItem> InventoryItems;
    [SerializeField] int MaxSlot => InventoryUIHandler.Instance.ItemSlots.Count;

    public PlayerInventorySlot WeaponSlot;
    public PlayerInventorySlot ShieldSlot;

    public void AddItem(InventoryItem Item)
    {
        InventoryItems.Add(Item);
        InventoryUIHandler.Instance.AddItemToUI(Item);
        OnItemAdded?.Invoke();
    }

    public void RemoveItem(InventoryItem Item, string slotID)
    {
        InventoryItems.Remove(Item);
        InventoryUIHandler.Instance.RemoveItemFromUI(Item, slotID);
        OnItemRemoved?.Invoke();
    }

    public bool PickUpItem(InventoryItem Item)
    {
        if (InventoryItems.Count < MaxSlot)
        {
            AddItem(Item);
            Debug.Log("ITEM IN INVENTORY IS NOW " + InventoryItems.Count +"\n MAX ITEMS IN INVENTORY IS " + MaxSlot);
            return true;
        }
        else return false;
    }

}
