using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using TMPro;
using System;

public class ItemInfoChanger : MonoBehaviour
{
    [SerializeField] List<TMProUGUIWithName> Texts;

    private void Awake()
    {
        EmptyInformation();
    }
    public void EmptyInformation()
    {
        for (int i = 0; i < Texts.Count; i++)
        {
            Texts[i].TMProUGUIElement.text = "";
        }
    }
    public void ChangeItemInformation(InventoryItem data)
    {
        if (Texts.Find(_ => _.StringName == "Item Name") != null) Texts.Find(_ => _.StringName == "Item Name").TMProUGUIElement.text = data.ItemName;
        if (Texts.Find(_ => _.StringName == "Item Description") != null) Texts.Find(_ => _.StringName == "Item Description").TMProUGUIElement.text = data.ItemDescription;
    }

}
[Serializable]
public class TMProUGUIWithName
{
    public TextMeshProUGUI TMProUGUIElement;
    public string StringName;
}
