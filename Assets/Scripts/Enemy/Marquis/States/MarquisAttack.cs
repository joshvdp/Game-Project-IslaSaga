using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using InterfaceAndInheritables;

namespace StateMachine.Enemy.State
{
    [CreateAssetMenu(fileName = "Marquis Attack", menuName = "State Machine/Enemy/Marquis/State/Attack")]
    public class MarquisAttack : MarquisMachineData
    {
        [SerializeField, Foldout("Attack")] private float damage = 5f;
        [SerializeField, Foldout("Attack")] LayerMask raycastableLayer;
        public float Damage => damage;
        public LayerMask RaycastableLayer => raycastableLayer;
        public override MarquisMachineFunctions Initialize(MarquisMonoStateMachine machine)
        {
            return new MarquisAttackFunctions(machine, this);
        }
    }

    public class MarquisAttackFunctions : MarquisMachineFunctions
    {
        float Damage;
        LayerMask RaycastableLayer;
        public MarquisAttackFunctions(MarquisMonoStateMachine machine, MarquisAttack data) : base(machine, data)
        {
            machine.Agent.isStopped = true;
            machine.LookAtTarget();
            Damage = data.Damage;
            RaycastableLayer = data.RaycastableLayer;
            machine.AnimationEvents.FindEvent("On Attack Frame").AddListener(Attack);
        }

        public void Attack()
        {
            RaycastHit hit;
            if (machine.AttackRange.NearestGameobject() == null) return;
                if (Physics.Raycast(machine.transform.position + Vector3.up * 0.5f, machine.AttackRange.NearestGameobject().transform.position
                    - machine.transform.position, out hit, 2f, RaycastableLayer))
                {
                    Shield shield = hit.collider.GetComponent<Shield>();
                    // If there is something blocking it, and it is a shield, get the shield's damage reduction and apply it to the damage.
                    if (shield != null)
                    {
                        machine.AttackCol.AttackAllTargets(Damage * shield.DamageReduction, DamageType.MELEE);
                        shield.OnShieldHit?.Invoke();
                    }
                    else machine.AttackCol.AttackAllTargets(Damage, DamageType.MELEE);
                }
                
            else Debug.Log("IDAMAGEABLE IS MISSING ON " + machine.AttackRange.NearestGameobject());

            
        }

        public override void Discard()
        {
            base.Discard();
            machine.AnimationEvents.FindEvent("On Attack Frame").RemoveListener(Attack);
        }

    }
}
