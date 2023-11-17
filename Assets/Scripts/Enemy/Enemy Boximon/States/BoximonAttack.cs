using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InterfaceAndInheritables;
namespace StateMachine.Enemy.State
{
    [CreateAssetMenu(fileName = "Boximon Attack", menuName = "State Machine/Enemy/Boximon/State/Attack")]
    public class BoximonAttack : BoximonMachineData
    {
        [SerializeField, Foldout("Attack")] private float damage = 5f;
        [SerializeField, Foldout("Attack")] LayerMask raycastableLayer;
        public float Damage => damage;
        public LayerMask RaycastableLayer => raycastableLayer;

        public override BoximonMachineFunctions Initialize(BoximonMonoStateMachine machine)
        {
            return new BoximonAttackFunctions(machine, this);
        }
    }

    public class BoximonAttackFunctions : BoximonMachineFunctions
    {
        float Damage;
        LayerMask RaycastableLayer;
        public BoximonAttackFunctions(BoximonMonoStateMachine machine, BoximonAttack data) : base(machine, data)
        {
            Damage = data.Damage;
            RaycastableLayer = data.RaycastableLayer;
            //machine.transform.LookAt(machine.CurrentTarget);
            machine.LookAtTarget();
            machine.Agent.isStopped = true;
            machine.AnimationEvents.FindEvent("Attack Frame").AddListener(Attack);
        }

        public void Attack()
        {
            for (int i = 0; i < machine.AttackCollider.ObjectsToDamage.Count; i++)
            {
                Debug.DrawRay(machine.transform.position + Vector3.up * 0.5f, machine.AttackCollider.ObjectsToDamage[i].transform.position 
                    - machine.transform.position , Color.red, 5f);
                if (machine.AttackCollider.ObjectsToDamage[i].GetComponent<IDamageable>() != null)
                {
                    // Check if Objects are Damageable
                    RaycastHit hit;
                    // Check if object to damage is right in front, and nothing is blocking it.
                    if (Physics.Raycast(machine.transform.position + Vector3.up * 0.5f, machine.AttackCollider.ObjectsToDamage[i].transform.position
                        - machine.transform.position, out hit,2f, RaycastableLayer))
                    {
                        Shield shield = hit.collider.GetComponent<Shield>();
                        // If there is something blocking it, and it is a shield, get the shield's damage reduction and apply it to the damage.
                        if (shield != null)
                        {
                            machine.AttackCollider.ObjectsToDamage[i].GetComponent<IDamageable>().Hit(Damage * shield.DamageReduction, DamageType.MELEE);
                            shield.OnShieldHit?.Invoke();
                        }
                        else machine.AttackCollider.ObjectsToDamage[i].GetComponent<IDamageable>().Hit(Damage, DamageType.MELEE);
                    }
                }
                
                else Debug.Log("IDAMAGEABLE IS MISSING ON " + machine.AttackCollider.ObjectsToDamage[i]);
                
            }
        } 
        public override void Discard()
        {
            base.Discard();
            machine.AnimationEvents.FindEvent("Attack Frame").RemoveListener(Attack);
        }

    }
}