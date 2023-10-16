using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Interface
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
    }
    public abstract class Shield: MonoBehaviour
    {
        public float DamageReduction;
        private void Awake() => GetComponent<Collider>().enabled = false;
        public abstract void Block();
    }
}
