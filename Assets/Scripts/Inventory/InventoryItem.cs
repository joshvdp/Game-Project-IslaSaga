using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InterfaceAndInheritables;
using System;
using NaughtyAttributes;
namespace Items
{
    [CreateAssetMenu(fileName = "Item Data", menuName = "Item/Item Data (Pickupable)")]
    public class InventoryItem : ScriptableObject
    {
        public ItemType ItemType;

        public ConsumableItemType ConsumableType;
        [Foldout("If Consumable")] public GameObject BehaviorObject;

        public EquippableItemType EquippableType;
        [Foldout("If Equippable")] public GameObject ItemToEquip;

        public string ItemName;
        public Sprite ItemImage;
        public GameObject ObjectModel;

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
        SHIELD,
        ACCESSORIES
    }

}
