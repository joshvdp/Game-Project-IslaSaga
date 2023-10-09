using System.Collections.Generic;
using UnityEngine;
using Interface;

namespace Items.Weapon
{
    public class AmethystSwordCS : MonoBehaviour, IWeapon
    {
        [SerializeField] float WeaponDamage;
        [SerializeField] float WeaponSequenceResetTime;
        public DamageType WeaponDamageType { get; set; }
        public float Damage { get {return WeaponDamage;} set {WeaponDamage = value; } }

        public float SequenceResetTime { get => WeaponSequenceResetTime; set { WeaponSequenceResetTime = value; } }

        
        public void Attack()
        {

        }

        public GameObject GetGameobject()
        {
            return gameObject;
        }
    }
}

