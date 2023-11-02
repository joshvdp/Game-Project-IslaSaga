using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Items;
using Manager;
using System;

public class PlayerInventorySlot : MonoBehaviour
{
    public Action OnUseItemOnSlot;
    public PlayerInventory ParentMainInventory => GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();
    public InventoryItem ItemData;
    public Image ItemImage;
    public GameObject BehaviorObject;
    public bool IsOccupied = false;

    public string SlotID => GetInstanceID().ToString();
    public void InitializeItem()
    {
        BehaviorObject = Instantiate(ItemData.BehaviorObject, transform);
    }
    void UnsubscribeObject() => BehaviorObject.SetActive(false);
    public void RemoveItemBehavior()
    {
        Destroy(BehaviorObject);
        BehaviorObject = null;
    }    
    public void UseItem()
    {
        if (ItemData == null) return;
        OnUseItemOnSlot?.Invoke();
        UnsubscribeObject();
        ParentMainInventory.RemoveItem(ItemData, SlotID);
    }
}
