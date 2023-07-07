using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interface.Weapon;
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] ControlBindings Controls;

    [SerializeField] Transform WeaponHolder;
    private void Start()
    {
        Debug.Log(Controls.Attack1Key);
    }
    private void Update()
    {
        Attack();
    }

    void Attack()
    {
        if(Input.GetKeyDown(Controls.Attack1Key))
        {
            WeaponHolder.GetChild(0).GetComponent<IWeapon>().Attack();
        }
    }
}
