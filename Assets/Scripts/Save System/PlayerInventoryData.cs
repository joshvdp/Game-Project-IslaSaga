using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using System.IO;
using System;

[CreateAssetMenu(fileName = "Player Inventory Data", menuName = "Player Inventory Data")]
[Serializable]
public class PlayerInventoryData : ScriptableObject
{
    public List<InventoryItem> InventoryItemsData;
    [SerializeField]
    public List<PlayerInventorySlot> InventorySlotsData;

    public PlayerInventorySlot WeaponSlot;
    public PlayerInventorySlot ShieldSlot;
    
    public InventoryItem WeaponData;
    public InventoryItem ShieldData;
    private void Awake()
    {
        ResetInventory();
    }
    public void UpdateInventory(List<InventoryItem> InventoryToSave)
    {
        InventoryItemsData = InventoryToSave;
    }

    public void UpdateInventoryUI(List<PlayerInventorySlot> InventorySlotsUIData)
    {
        InventorySlotsData = InventorySlotsUIData;
    }

    public void ResetInventory()
    {
        InventoryItemsData.Clear();
        InventorySlotsData.Clear();
        WeaponSlot = null;
        WeaponData = null;
        ShieldSlot = null;
        ShieldData = null;
        //Debug.Log("INVENTORY CLEARED");
    }

    public void SaveList()
    {
        PlayerInventorySlotData data = new PlayerInventorySlotData();
        List<InventoryItem> inventoryitemdata = new List<InventoryItem>();

        for (int i = 0; i < InventorySlotsData.Count; i++)
        {
            Debug.Log(" PASSED " + i + " TIME " + InventorySlotsData[i].ItemData);
            inventoryitemdata.Add(InventorySlotsData[i].ItemData);
        }

        data.PlayerInventorySavedItemData = inventoryitemdata;

        data.ShieldData = ShieldData;

        data.WeaponData = WeaponData;


        string json = JsonUtility.ToJson(data, true);
        if (!File.Exists(Application.persistentDataPath + "/SaveData.json")) File.WriteAllText(Application.persistentDataPath + "/SaveData.json", json);
        else
        {
            JsonUtility.FromJsonOverwrite(json, Application.persistentDataPath + "/SaveData.json");
            Debug.Log("SAVE OVERWRITED");
        }
        Debug.Log("INVENTORY SAVED " + json);
    }

    
}
