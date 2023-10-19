using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InterfaceAndInheritables;
namespace StateMachine.Enemy.State
{
    [CreateAssetMenu(fileName = "Fiery Attack", menuName = "State Machine/Enemy/Fiery/State/Attack")]
    public class FieryAttack : FieryMachineData
    {
        [SerializeField, Foldout("Attack")] private float damage = 5f;
        [SerializeField, Foldout("Attack")] LayerMask raycastableLayer;
        public float Damage => damage;
        public LayerMask RaycastableLayer => raycastableLayer;
        public override FieryMachineFunctions Initialize(FieryMonoStateMachine machine)
        {
            return new FieryAttackFunctions(machine, this);
        }
    }

    public class FieryAttackFunctions : FieryMachineFunctions
    {
        float Damage;
        LayerMask RaycastableLayer;
        public FieryAttackFunctions(FieryMonoStateMachine machine, FieryAttack data) : base(machine, data)
        {
            Damage = data.Damage;
            RaycastableLayer = data.RaycastableLayer;
            machine.LookAtTarget();
            machine.Agent.isStopped = true;
            machine.AnimationEvents.FindEvent("Attack Frame").AddListener(Attack);
        }

        public void Attack()
        {
            for (int i = 0; i < machine.AttackCollider.ObjectsToDamage.Count; i++)
            {
                if (machine.AttackCollider.ObjectsToDamage[i].GetComponent<IDamageable>() != null)
                {
                    RaycastHit hit;
                    if(Physics.Raycast(machine.transform.position, machine.AttackCollider.ObjectsToDamage[i].transform.position - machine.transform.position,out hit, RaycastableLayer))
                    {
                        Debug.Log("HIT SOMETHING " + hit.collider.name);
                    }
                    machine.AttackCollider.ObjectsToDamage[i].GetComponent<IDamageable>().Hit(Damage, DamageType.MELEE);
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