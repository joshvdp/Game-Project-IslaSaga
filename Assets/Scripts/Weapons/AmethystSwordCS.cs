using System.Collections.Generic;
using UnityEngine;
using InterfaceAndInheritables;
using UnityEngine.Events;

namespace Items.Weapon
{
    public class AmethystSwordCS : MonoBehaviour, IWeapon
    {
        [SerializeField] float WeaponDamage;
        [SerializeField] float WeaponSequenceResetTime;
        [SerializeField] Quaternion WeaponRotationVar;
        [SerializeField] DamageType WeaponDmgType;
        public DamageType WeaponDamageType { get { return WeaponDmgType; } set { WeaponDmgType = value; } }
        public float Damage { get {return WeaponDamage;} set {WeaponDamage = value; } }

        public float SequenceResetTime { get => WeaponSequenceResetTime; set { WeaponSequenceResetTime = value; } }

        public Quaternion WeaponRotation { get => WeaponRotationVar; set { WeaponRotationVar = value; } }

        public void Attack()
        {
            Debug.Log("Attacking");
        }

        public GameObject GetGameobject() => gameObject;
        public void DestroyThisWeapon() => DestroyImmediate(gameObject);

        //public UnityEvent Events;
    }
}

