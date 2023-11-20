using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystemJSON : MonoBehaviour
{
    public static SaveSystemJSON Instance;
    [SerializeField] PlayerInventoryData InventoryData;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        
    }

    public void DeleteSaveData()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveData.json"))
        {
            File.Delete(Application.persistentDataPath + "/SaveData.json");
            Debug.Log("Deleted save");
        }
    }
    public void SaveData()
    {
        if(File.Exists(Application.persistentDataPath + "/SaveData.json"))
        {
            File.Delete(Application.persistentDataPath + "/SaveData.json");
            Debug.Log("Deleted save");
        }
        InventoryData.SaveList();


    }
    public string ReadJson(string Path)
    {
        if (File.Exists(Path)) return File.ReadAllText(Path);
        else if (!File.Exists(Path))
        {
            Debug.Log("NO FILE IS FOUND IN " + Path);
            return null;
        }
        else return null;
    }
}
