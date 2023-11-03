using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InterfaceAndInheritables;
using System;

namespace Items
{
    [CreateAssetMenu(fileName = "Item Data", menuName = "Item/Item Data (Pickupable)")]
    public class InventoryItem : ScriptableObject
    {
        public ItemType ItemType;
        public ConsumableItemType ConsumableType;
        public EquippableItemType EquippableType;

        public GameObject BehaviorObject;

        public string ItemName;
        public Sprite ItemImage;

    }

    public enum ItemType
    {
        CONSUMABLE,
        EQUIPPABLE,
        QUEST_ITEM
    }

    public enum ConsumableItemType
    {
        NONE,
        HP,
        MANA
    }

    public enum EquippableItemType
    {
        NONE,
        ARMOR,
        WEAPON,
        ACCESSORIES
    }

}
