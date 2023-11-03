using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Items;
using Manager;
using System;
using UnityEngine.EventSystems;
public class PlayerInventorySlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public Action OnUseItemOnSlot;
    [SerializeField] CanvasGroup canvasGroup;
    public PlayerInventory ParentMainInventory => GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();
    public InventoryItem ItemData;
    public Image ItemImage;
    public GameObject BehaviorObject;
    [SerializeField] GameObject DraggableObject;

    GameObject DraggableObjectInstance;
    RectTransform DraggableInstanceRect;

    public bool IsOccupied = false;
    public string SlotID => GetInstanceID().ToString();

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

    public void InitializeItem()
    {
        BehaviorObject = Instantiate(ItemData.BehaviorObject, transform);
        ItemImage.sprite = ItemData.ItemImage;
        IsOccupied = true;
    }

    
    public void RemoveItemFromSlot()
    {
        RemoveItemBehavior();
        ItemData = null;
        ItemImage.sprite = null;
        IsOccupied = false;
        Destroy(DraggableObjectInstance);
    }
    public void RemoveItemBehavior()
    {
        BehaviorObject.SetActive(false);
        Destroy(BehaviorObject);
        BehaviorObject = null;
    }    
    public void UseItem()
    {
        if (ItemData == null) return;
        OnUseItemOnSlot?.Invoke();
        ParentMainInventory.RemoveItem(ItemData, SlotID);
    }
    void StartItemDrag()
    {
        canvasGroup.blocksRaycasts = false;
        DraggableObjectInstance = Instantiate(DraggableObject, Input.mousePosition, Quaternion.identity, transform.parent.parent.parent).gameObject;
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
        DraggableInstanceRect.anchoredPosition += eventData.delta;
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
            PlayerInventorySlot TempSlotHolder;

            if (!IsOccupied) // Check if current slot is NOT occupied
            {
                // Set the item data on this slot
                ItemData = Item;
                InitializeItem();
                //

                //Remove item from previous slot
                OtherSlot.RemoveItemFromSlot();
                OtherSlot.canvasGroup.blocksRaycasts = true;
                //
                Debug.Log("ITEM IS DROPPED ON THIS SLOT ");
            } else if (IsOccupied)
            {
                TempSlotHolder = OtherSlot; // Hold Other Slots data
                // Change other slot data to this
                OtherSlot.RemoveItemFromSlot();
                OtherSlot.ItemData = ItemData;
                OtherSlot.InitializeItem();
                //
                //Change THIS slot data to the other slot data.
                RemoveItemFromSlot();
                ItemData = OtherSlot.ItemData;
                InitializeItem();
                //
                TempSlotHolder = null; // Set to null just to be safe.
                Debug.Log("ITEMS SUCCESSFULLY SWAPPED");
            }
        }
    }
    #endregion
}
