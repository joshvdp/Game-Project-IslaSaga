using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interface;
public class WoodenShield : MonoBehaviour, IShield
{
    [SerializeField] float DamageBlockReduction;
    public float DamageReduction { get { return DamageBlockReduction; } set { DamageBlockReduction = value; } }

    public void Block()
    {

    }
}
