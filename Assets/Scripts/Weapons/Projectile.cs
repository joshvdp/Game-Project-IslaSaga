using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interface;

namespace Items.Weapon
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Projectile : MonoBehaviour, IProjectile
    {
        public float DefaultSpeed;
        public float DefaultDamage;
        public Rigidbody rb { get { return GetComponent<Rigidbody>(); } set { value = GetComponent<Rigidbody>(); } }
        public float Speed { get { return DefaultSpeed; } set { DefaultSpeed = value; }}
        public float Damage { get { return DefaultDamage; } set { DefaultDamage = value; } }

        private void OnCollisionEnter(Collision collision)
        {
            IDamageable target = collision.transform.GetComponent<IDamageable>();
            Rigidbody targetRb = collision.transform.GetComponent<Rigidbody>();
            if (target != null) target.Hit(Damage, DamageType.RANGE);
            if (targetRb != null) targetRb.AddForce((rb.velocity + targetRb.velocity), ForceMode.VelocityChange);

            Destroy(gameObject);
        }
    }

}
