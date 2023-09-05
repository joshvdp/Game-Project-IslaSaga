using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interface;
namespace StateMachine.Enemy.State
{
    [CreateAssetMenu(fileName = "Boximon Attack", menuName = "State Machine/Enemy/Boximon/State/Attack")]
    public class BoximonAttack : BoximonMachineData
    {
        [SerializeField, Foldout("Attack")] private float damage = 5f;
        public float Damage => damage;
        public override BoximonMachineFunctions Initialize(BoximonMonoStateMachine machine)
        {
            return new BoximonAttackFunctions(machine, this);
        }
    }

    public class BoximonAttackFunctions : BoximonMachineFunctions
    {
        float Damage;
        public BoximonAttackFunctions(BoximonMonoStateMachine machine, BoximonAttack data) : base(machine, data)
        {
            Damage = data.Damage;
            //machine.transform.LookAt(machine.CurrentTarget);
            machine.LookAtTarget();
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