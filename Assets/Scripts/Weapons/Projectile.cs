using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InterfaceAndInheritables;

namespace Items.Weapon
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Projectile : MonoBehaviour, IProjectile
    {
        [SerializeField] LayerMask RaycastableLayer;
        public float DefaultSpeed;
        public float DefaultDamage;
        public float DefaultArmorPenetrationPercent;
        public Rigidbody rb { get { return GetComponent<Rigidbody>(); } set => value = GetComponent<Rigidbody>(); } 
        public float Speed { get { return DefaultSpeed; } set { DefaultSpeed = value; }}
        public float Damage { get { return DefaultDamage; } set { DefaultDamage = value; } }

        public float ArmorPenetrationPercent { get { return DefaultArmorPenetrationPercent; } set { DefaultArmorPenetrationPercent = value; } }

        private void OnCollisionEnter(Collision collision)
        {
            IDamageable target = collision.transform.GetComponent<IDamageable>();
            Rigidbody targetRb = collision.transform.GetComponent<Rigidbody>();
            if (target != null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + Vector3.up * 0.5f, targetRb.transform.position
                           - transform.position, out hit, 2f, RaycastableLayer))
                {
                    Shield shield = hit.collider.GetComponent<Shield>();

                    // If there is something blocking it, and it is a shield, get the shield's damage reduction and apply it to the damage.
                    if (shield != null)
                    {
                        target.Hit(Damage * Mathf.Clamp(shield.DamageReduction + ArmorPenetrationPercent, 0, 1), DamageType.RANGE);
                        Debug.Log(Mathf.Clamp(shield.DamageReduction + ArmorPenetrationPercent, 0, 1));
                    }
                    else target.Hit(Damage, DamageType.RANGE);
                }
            }
            if (targetRb != null) targetRb.AddForce((rb.velocity + targetRb.velocity), ForceMode.VelocityChange);

            Destroy(gameObject);
        }
    }

}
