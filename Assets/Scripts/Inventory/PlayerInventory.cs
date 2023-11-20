using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using UnityEngine.Events;
public class PlayerInventory : MonoBehaviour
{
    [SerializeField] UnityEvent OnItemAdded;
    [SerializeField] UnityEvent OnItemRemoved;

    public PlayerInventoryData InventoryData;

    public List<InventoryItem> InventoryItems;
    [SerializeField] int MaxSlot => InventoryUIHandler.Instance.ItemSlots.Count;


    private void Awake()
    {
        InventoryData.ResetInventory();
        InventoryItems = InventoryData.InventoryItemsData;
    }
    public void AddItem(InventoryItem Item)
    {
        InventoryItems.Add(Item);
        InventoryUIHandler.Instance.AddItemToUI(Item);
        OnItemAdded?.Invoke();
        InventoryData.UpdateInventory(InventoryItems);
    }

    public void RemoveItem(InventoryItem Item, string slotID)
    {
        InventoryItems.Remove(Item);
        InventoryUIHandler.Instance.RemoveItemFromUI(Item, slotID);
        OnItemRemoved?.Invoke();
        InventoryData.UpdateInventory(InventoryItems);
    }

    public bool PickUpItem(InventoryItem Item)
    {
        if (InventoryItems.Count < MaxSlot)
        {
            AddItem(Item);
            return true;
        }
        else return false;
    }

}
