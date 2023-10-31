using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Items;
namespace InterfaceAndInheritables
{
    public enum DamageType
    {
        MELEE,
        RANGE
    }
    public interface IDamageable
    {
        UnityEvent onDeath { get; set; }
        float MaxHealth { get; set; }
        float CurrentHealth { get; set; }

        bool IsDamageable { get; set; }
        void Hit(float Damage, DamageType Type);

    }
    public interface IWeapon
    {
        DamageType WeaponDamageType { get; set; }
        float Damage { get; set; }
        float SequenceResetTime { get; set; }
        void Attack();
        GameObject GetGameobject();
    }

    // OLD INTERFACE
    public interface IShield
    {
        float DamageReduction { get; set; }
        void Block();

    }
    //

   
    public interface IProjectile
    {
        Rigidbody rb { get; set; }
        float Speed { get; set; }
        float Damage { get; set; }

        float ArmorPenetrationPercent { get; set; }
    }
    public abstract class Shield: MonoBehaviour
    {
        public float DamageReduction;
        private void Awake() => GetComponent<Collider>().enabled = false;
        public abstract void Block();
    }

    [Serializable] 
    public class Droppable
    {
        public GameObject ItemToDrop;
        public float Probability;
    }

    //public abstract class Pickupable: MonoBehaviour
    //{
    //    public Rigidbody rb => GetComponent<Rigidbody>();
    //    public InventoryItem ItemData;
    //}

    public abstract class Consumable : MonoBehaviour
    {
        private void OnEnable()
        {
            transform.parent.GetComponent<PlayerInventorySlot>().OnUseItemOnSlot += UseConsumable;
        }
        private void OnDisable()
        {
            transform.parent.GetComponent<PlayerInventorySlot>().OnUseItemOnSlot -= UseConsumable;
        }
        public abstract void UseConsumable();
    }
}
