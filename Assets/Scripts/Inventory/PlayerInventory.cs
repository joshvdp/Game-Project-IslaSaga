using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using UnityEngine.Events;
using Player.Controls;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] UnityEvent OnItemAdded;
    [SerializeField] UnityEvent OnItemRemoved;

    public PlayerInventoryData InventoryData;

    public List<InventoryItem> InventoryItems;
    [SerializeField] int MaxSlot => InventoryUIHandler.Instance.ItemSlots.Count;

    PlayerInputs _playerInputs;
    PlayerInputs PlayerInputs => _playerInputs ? _playerInputs : _playerInputs = GetComponent<PlayerInputs>();

    private void OnEnable()
    {
        PlayerInputs.OnUseHPPotion += QuickUseHpPotion;
    }

    private void OnDisable()
    {
        PlayerInputs.OnUseHPPotion -= QuickUseHpPotion;
    }
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

    public void QuickUseHpPotion()
    {
        InventoryUIHandler.Instance.ItemSlots.Find(_ => _.ItemData?.ConsumableType == ConsumableItemType.HP)?.UseItem();
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
