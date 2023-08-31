using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Combat;
namespace Interface
{
    public interface IWeapon
    {
        float Damage { get; set;}
        void Attack(List<GameObject> targets,int Sequence);
    }
}
