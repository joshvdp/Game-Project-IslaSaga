using UnityEngine;
using InterfaceAndInheritables;

namespace Items.Weapon
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Projectile : MonoBehaviour, IProjectile
    {
        [SerializeField] LayerMask RaycastableLayer;

        public float DefaultDamage;
        public float DefaultArmorPenetrationPercent;
        public Rigidbody rb { get { return GetComponent<Rigidbody>(); } set => value = GetComponent<Rigidbody>(); } 
        public float Speed { get; set;}
        public float Damage { get { return DefaultDamage; } set { DefaultDamage = value; } }
        public float ArmorPenetrationPercent { get { return DefaultArmorPenetrationPercent; } set { DefaultArmorPenetrationPercent = value; } }

        public float KnockbackMultiplier = 0.3f;

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
                    if (shield != null) target.Hit(Damage * Mathf.Clamp(shield.DamageReduction + ArmorPenetrationPercent, 0, 1), DamageType.RANGE);
                    else target.Hit(Damage, DamageType.RANGE);
                }
            }
            if (targetRb != null) ApplyKnockback(targetRb);

            Destroy(gameObject);
        }

        void ApplyKnockback(Rigidbody rb)
        {
            Vector3 knockbackAmount = rb.velocity + rb.velocity;
            knockbackAmount = new Vector3(knockbackAmount.x, 0, knockbackAmount.z); // Negates force in the Y-axis
            rb.AddForce(knockbackAmount * KnockbackMultiplier, ForceMode.Impulse);
        }
    }

}
