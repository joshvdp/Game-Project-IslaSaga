using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Combat;
namespace Interface
{
    public interface IWeapon
    {
        float Damage { get; set;}
        float SequenceResetTime { get; set; }
        void Attack();

        GameObject GetGameobject();
    }
}
