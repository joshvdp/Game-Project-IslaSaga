using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interface;
namespace StateMachine.Enemy.State
{
    [CreateAssetMenu(fileName = "Fiery Attack", menuName = "State Machine/Enemy/Fiery/State/Attack")]
    public class FieryAttack : FieryMachineData
    {
        [SerializeField, Foldout("Attack")] private float damage = 5f;
        public float Damage => damage;
        public override FieryMachineFunctions Initialize(FieryMonoStateMachine machine)
        {
            return new FieryAttackFunctions(machine, this);
        }
    }

    public class FieryAttackFunctions : FieryMachineFunctions
    {
        float Damage;
        public FieryAttackFunctions(FieryMonoStateMachine machine, FieryAttack data) : base(machine, data)
        {
            Damage = data.Damage;
            machine.Agent.isStopped = true;
            machine.AnimationEvents.FindEvent("Attack Frame").AddListener(Attack);
        }

        public void Attack()
        {
            for (int i = 0; i < machine.AttackCollider.ObjectsToDamage.Count; i++)
            {
                if (machine.AttackCollider.ObjectsToDamage[i].GetComponent<IDamageable>() != null)
                     machine.AttackCollider.ObjectsToDamage[i].GetComponent<IDamageable>().Hit(Damage);
                
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