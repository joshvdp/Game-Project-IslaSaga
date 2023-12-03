using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Items;
using Manager;
using System;
using UnityEngine.EventSystems;
using StateMachine.Player;
using InterfaceAndInheritables;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerInventorySlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public UnityEvent OnItemAdded;
    public UnityEvent OnItemRemoved;

    public Action OnUseItemOnSlot;
    [SerializeField] CanvasGroup canvasGroup;
    public PlayerInventory ParentMainInventory => GameObject.Find("Player").GetComponent<PlayerInventory>();
    [SerializeField] Transform InventoryUIParent;
    public InventoryItem ItemData;
    public Image ItemImage;
    public GameObject BehaviorObject;
    [SerializeField] GameObject DraggableObject;

    GameObject DraggableObjectInstance;
    RectTransform DraggableInstanceRect;

    public bool IsOccupied = false;
    public bool IsEquipSlot = false;
    bool HasInitialized = false;
    public EquippableItemType ItemSlotEquipType;
    public string SlotID => GetInstanceID().ToString();

    Canvas MainCanvas => transform.root.GetComponent<Canvas>();

    private void Awake()
    {
        InitializeItem();
    }
    private void OnEnable()
    {

    }
    private void OnDisable()
    {
        if (DraggableObjectInstance != null)
        {
            Destroy(DraggableObjectInstance);
            canvasGroup.blocksRaycasts = true;
            
        }
    }
    public Transform EquippableTransform;
    public void InitializeItem()
    {
        
        if (ItemData == null || HasInitialized) return;
        switch (ItemData.ItemType)
        {
            case ItemType.CONSUMABLE:
                BehaviorObject = Instantiate(ItemData.BehaviorObject, transform);
                OnUseItemOnSlot += BehaviorObject.GetComponent<Consumable>().UseConsumable;
                break;
            case ItemType.EQUIPPABLE:
                
                if (IsEquipSlot)
                {
                    PlayerMonoStateMachine PlayerMachine = ParentMainInventory.GetComponent<PlayerMonoStateMachine>();
                    Transform Item;
                    switch (ItemData.EquippableType)
                    {
                        case EquippableItemType.WEAPON:
                            EquippableTransform = Instantiate(ItemData.ItemToEquip, PlayerMachine.WeaponHolderPosition.position,
                                                Quaternion.identity, PlayerMachine.WeaponHolderPosition).transform;
                            EquippableTransform.localRotation = EquippableTransform.GetComponent<IWeapon>().WeaponRotation;
                            EquippableTransform.localPosition = Vector3.zero;
                            PlayerMachine.WeaponOnHand = PlayerMachine.WeaponHolderPosition?.GetComponentInChildren<IWeapon>();
                            break;
                        case EquippableItemType.SHIELD:
                            Item = Instantiate(ItemData.ItemToEquip, PlayerMachine.ShieldHolderPosition.position,
                                                Quaternion.identity, PlayerMachine.ShieldHolderPosition).transform;
                            Item.localRotation = Item.GetComponent<Shield>().ShieldRotation;
                            Item.localPosition = Vector3.zero;
                            break;
                        case EquippableItemType.ACCESSORIES:
                            break;
                        case EquippableItemType.ARMOR:
                            break;

                    }
                    if (transform.parent.GetComponent<ItemInfoChanger>()) transform.parent.GetComponent<ItemInfoChanger>().ChangeItemInformation(ItemData);
                    InventoryUIHandler.Instance.AddEquippableToSavableData(ItemData, this);
                    PlayerMachine.AssignWeaponAndOrShield();
                }
                break;
            case ItemType.QUEST_ITEM:
                break;
        }
        
        OnItemAdded?.Invoke();
        ItemImage.sprite = ItemData.ItemImage;
        IsOccupied = true;
        HasInitialized = true;
    }

    public void RemoveEquippable()
    {
        Debug.Log("REMOVE EQUIPPABLE CALLED");
        PlayerMonoStateMachine PlayerMachine = ParentMainInventory.GetComponent<PlayerMonoStateMachine>();
        if (IsEquipSlot)
        {
            switch (ItemData.EquippableType)
            {
                case EquippableItemType.WEAPON:
                    if (PlayerMachine.WeaponOnHandGameObject.gameObject == null) break;
                    DestroyImmediate(PlayerMachine.WeaponOnHandGameObject.gameObject); // For some reason "Destroy" function doesn't destroy the instance of the object (object destroyed can still be reference but can't be seen.)
                    break;
                case EquippableItemType.SHIELD:
                    if (PlayerMachine.ShieldCollider.gameObject == null) break;
                    DestroyImmediate(PlayerMachine.ShieldCollider.gameObject);
                    break;
                case EquippableItemType.ACCESSORIES:
                    break;
                case EquippableItemType.ARMOR:
                    break;
            }
            PlayerMachine.AssignWeaponAndOrShield();
        }
        ItemImage.sprite = null;
        IsOccupied = false;
    }
    public void RemoveItemFromSlot()
    {
        if (ItemData == null) return;
        if (IsEquipSlot) RemoveEquippable();
        RemoveItemBehavior();
        ItemData = null;
        ItemImage.sprite = null;
        IsOccupied = false;
        OnItemRemoved?.Invoke();
        Destroy(DraggableObjectInstance);
        HasInitialized = false;

    }
    public void RemoveItemBehavior()
    {
        if (ItemData.ItemType != ItemType.CONSUMABLE) return;
        BehaviorObject.SetActive(false);
        Destroy(BehaviorObject);
        BehaviorObject = null;
    }    
    public void UseItem()
    {
        if (ItemData == null || ItemData.ItemType != ItemType.CONSUMABLE) return;
        OnUseItemOnSlot?.Invoke();
        Debug.Log("ITEM " + ItemData.name + " IS USED    " + "Parent Main Inventory is " + (ParentMainInventory != null));
        
        ParentMainInventory.RemoveItem(ItemData, SlotID);
        
    }
    void StartItemDrag()
    {
        canvasGroup.blocksRaycasts = false;
        DraggableObjectInstance = Instantiate(DraggableObject, Input.mousePosition, Quaternion.identity, InventoryUIParent).gameObject;
        DraggableObjectInstance.GetComponent<Image>().sprite = ItemData.ItemImage;
        DraggableObjectInstance.GetComponent<CanvasGroup>().alpha = 0.6f;
        DraggableInstanceRect = DraggableObjectInstance.GetComponent<RectTransform>();
    }

    #region Drag and Drop Pointer event functions
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!IsOccupied) return;
        StartItemDrag();
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!IsOccupied) return;
        if (DraggableInstanceRect == null)
        {
            eventData.pointerDrag = null;
            return;
        }
        DraggableInstanceRect.anchoredPosition += eventData.delta / MainCanvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!IsOccupied) return;
        Destroy(DraggableObjectInstance);
        canvasGroup.blocksRaycasts = true;
        
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<PlayerInventorySlot>() == null) return; //Prevents drop function from other drag functions to not be detected.
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<PlayerInventorySlot>().IsOccupied)
        {
            InventoryItem Item = eventData.pointerDrag.GetComponent<PlayerInventorySlot>().ItemData; // Get the itemdata
            PlayerInventorySlot OtherSlot = eventData.pointerDrag.GetComponent<PlayerInventorySlot>();
            InventoryItem TempItemDataHolder;

            if (!IsOccupied) // Check if current slot is NOT occupied
            {
                if (IsEquipSlot)
                {
                    if (Item.ItemType != ItemType.EQUIPPABLE) return;
                    if (Item.EquippableType != ItemSlotEquipType) return;
                }// Don't proceed if this is equip slot and item to transfer is NOT a equippable, AND/OR Item slot equip type is not equal to item equip type.
                
                // Set the item data on this slot
                ItemData = Item;
                InitializeItem();
                //

                //Remove item from previous slot
                OtherSlot.RemoveItemFromSlot();
                OtherSlot.canvasGroup.blocksRaycasts = true;
                //
            } 
            else if (IsOccupied)
            {
                if (IsEquipSlot)
                {
                    if (Item.ItemType != ItemType.EQUIPPABLE) return;
                    if (Item.EquippableType != ItemSlotEquipType) return;
                } // Don't proceed if this is equip slot and item to transfer is NOT a equippable, AND/OR Item slot equip type is not equal to item equip type.
                TempItemDataHolder = OtherSlot.ItemData;
                // Change other slot data to this
                OtherSlot.RemoveItemFromSlot();
                OtherSlot.ItemData = ItemData;
                OtherSlot.InitializeItem();
                //
                //Change THIS slot data to the other slot data.
                RemoveItemFromSlot();
                ItemData = TempItemDataHolder;
                InitializeItem();
                //
                TempItemDataHolder = null;
                Debug.Log("ITEMS SUCCESSFULLY SWAPPED");
            }
        }
    }
    #endregion
}
