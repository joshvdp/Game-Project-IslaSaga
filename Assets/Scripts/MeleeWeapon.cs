using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interface.Weapon;
public class MeleeWeapon : MonoBehaviour, IWeapon
{
    public void Attack()
    {
        Debug.Log("ATTACKING");
    }
    
}
