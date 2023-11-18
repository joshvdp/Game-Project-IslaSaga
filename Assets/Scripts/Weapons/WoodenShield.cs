using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InterfaceAndInheritables;
public class WoodenShield : Shield
{
    public override void Block()
    {
        //will call a sound effect method from here
        Debug.Log("ENEMY BLOCK");
    }
}
