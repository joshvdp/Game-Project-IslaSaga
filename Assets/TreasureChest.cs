using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InterfaceAndInheritables;
using StateMachine.Player;
using Items;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TreasureChest : Interactable
{
    public UnityEvent OnAbilityGive;
    public UnityEvent OnItemGive;
    [SerializeField] InventoryItem item;
    [SerializeField] PlayerMachineData AbilityToGive;
    [SerializeField] Animator ChestAnimator;
    [SerializeField] GameObject ItemInChest;
    public override void Interact(PlayerMonoStateMachine player)
    {
        if (!IsInteractable) return;

        base.Interact(player);

        ChestAnimator.SetTrigger("Open");
        Instantiate(item.ObjectModel, ItemInChest.transform.position, Quaternion.identity, ItemInChest.transform);
        ItemInChest.SetActive(false);
        GiveLoot(player.GetComponent<PlayerInventory>(), item);
        GiveAbility();
        ToggleInteractIcon(true);
        IsInteractable = false; // Makes sure to only interact once
        
    }

    void GiveLoot(PlayerInventory inventory, InventoryItem itemToGive)
    {
        if (itemToGive == null) return;
        inventory.AddItem(itemToGive);
        OnItemGive?.Invoke();
    }

    void GiveAbility()
    {
        if (AbilityToGive == null) return;
        AbilityToGive.isUnlocked = true;
        OnAbilityGive?.Invoke();
    }

}
