using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
public class InventoryUIHandler : MonoBehaviour
{
    public static InventoryUIHandler Instance;

    public Transform InventorySlotsParent; // To increase performance
    public List<PlayerInventorySlot> ItemSlots;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        ItemSlots.AddRange(InventorySlotsParent.GetComponentsInChildren<PlayerInventorySlot>());

        PlayerInventory ParentInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();

        for (int i = 0; i < ItemSlots.Count; i++)
        {
            ItemSlots[i].ParentMainInventory = ParentInventory;
        }
    }
    public void AddItemToUI(InventoryItem Item)
    {
        for (int i = 0; i < ItemSlots.Count; i++)
        {
            if (!ItemSlots[i].IsOccupied)
            {
                ItemSlots[i].ItemData = Item;
                ItemSlots[i].InitializeItem();
                break;
            }
        }
    }

    public void RemoveItemFromUI(InventoryItem Item, string slotID)
    {
        for (int i = 0; i < ItemSlots.Count; i++)
        {
            if (ItemSlots[i].ItemData == Item && ItemSlots[i].SlotID == slotID)
            {
                ItemSlots[i].RemoveItemFromSlot();
                break;
            }
        }
    }

}