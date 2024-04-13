using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InterfaceAndInheritables;
using Manager;
public class HealthPotionConsumable : Consumable
{
    public float HealAmount;
    public override void UseConsumable()
    {
        MainManager.Instance.PlayerStatsSCO.OnHpPotionUsed?.Invoke();
        MainManager.Instance.PlayerStatsSCO.TakeHeal(HealAmount);
    }
}
