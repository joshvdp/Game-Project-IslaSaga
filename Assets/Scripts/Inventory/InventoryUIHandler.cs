using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class InventoryUIHandler : MonoBehaviour
{
    public UnityEvent<InventoryItem> OnEquippableItemAdded;

    public static InventoryUIHandler Instance;
    public PlayerInventoryData InventoryData;

    public Transform InventorySlotsParent; // To increase performance

    [SerializeField] public List<PlayerInventorySlot> ItemSlots;
    [SerializeField] List<GameObject> itemSlotsGameObjects;

    public PlayerInventorySlot WeaponSlot;
    public PlayerInventorySlot ShieldSlot;

    public bool HasPotion = false;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoadFunctions;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoadFunctions;
    }
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

    }
    void OnSceneLoadFunctions(Scene scene, LoadSceneMode mode)
    {
        InitializeThis();
        AssignVarsFromSave();
        CheckIfPotionIsAvailable();
    }
    void InitializeThis()
    {
        ItemSlots.Clear();
        ItemSlots.AddRange(InventorySlotsParent.GetComponentsInChildren<PlayerInventorySlot>());
        WeaponSlot.RemoveItemFromSlot();
        ShieldSlot.RemoveItemFromSlot();
        CheckIfPotionIsAvailable();
    }
    void AssignVarsFromSave()
    {
        string json = SaveSystemJSON.Instance?.ReadJson(Application.persistentDataPath + "/SaveData.json");

        if (json == null || json == "{}") return;

        Debug.Log("Starting Loading Inventory");

        PlayerInventorySlotData fileSaved = JsonUtility.FromJson<PlayerInventorySlotData>(json);

        if (fileSaved.PlayerInventorySavedItemData.Count <= 0) return;

        for (int i = 0; i < ItemSlots.Count; i++)
        {
            ItemSlots[i].ItemData = fileSaved.PlayerInventorySavedItemData[i];
            if(fileSaved.PlayerInventorySavedItemData[i] != null) InventoryData.InventoryItemsData.Add(fileSaved.PlayerInventorySavedItemData[i]);
            ItemSlots[i].InitializeItem();
        }


        if (fileSaved.WeaponData != null)
        {
            WeaponSlot.ItemData = fileSaved.WeaponData;
            InventoryData.InventoryItemsData.Add(fileSaved.WeaponData);
        }
        if (fileSaved.ShieldData != null)
        {
            ShieldSlot.ItemData = fileSaved.ShieldData;
            InventoryData.InventoryItemsData.Add(fileSaved.ShieldData);
        }


        if (WeaponSlot.ItemData != null) WeaponSlot.InitializeItem();
        if (ShieldSlot.ItemData != null) ShieldSlot.InitializeItem();
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
        CheckIfPotionIsAvailable();
        InventoryData.UpdateInventoryUI(ItemSlots);
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
        CheckIfPotionIsAvailable();
        InventoryData.UpdateInventoryUI(ItemSlots);
    }

    void CheckIfPotionIsAvailable()
    {
        HasPotion = ItemSlots.Find(_ => _.ItemData?.ConsumableType == ConsumableItemType.HP);
    }
    public void AddEquippableToSavableData(InventoryItem Equippable, PlayerInventorySlot ItemSlot)
    {
        if(Equippable.EquippableType == EquippableItemType.WEAPON)
        {
            WeaponSlot.ItemData = Equippable;

            InventoryData.WeaponSlot = ItemSlot;
            InventoryData.WeaponData = Equippable;
        }

        if(Equippable.EquippableType == EquippableItemType.SHIELD)
        {
            ShieldSlot.ItemData = Equippable;

            InventoryData.ShieldSlot = ItemSlot;
            InventoryData.ShieldData = Equippable;
        }
    }

    public void SaveSlotList()
    {
        
        //PlayerInventorySlotData data = new PlayerInventorySlotData();
        //List<InventoryItem> inventoryitemdata = new List<InventoryItem>();
        //data.PlayerInventorySavableSlotData = ItemSlots;
        
        //for (int i = 0; i < ItemSlots.Count; i++)
        //{
        //    Debug.Log(" PASSED " + i + " TIME " + ItemSlots[i].ItemData);
        //    inventoryitemdata.Add(ItemSlots[i].ItemData);
        //}

        //data.PlayerInventoryItemData = inventoryitemdata;


        //string json = JsonUtility.ToJson(data, true);
        //File.WriteAllText(Application.persistentDataPath + "/SaveData.json", json);
        
    }

}
