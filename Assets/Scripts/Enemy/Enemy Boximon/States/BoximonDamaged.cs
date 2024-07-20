using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Enemy.State
{
    [CreateAssetMenu(fileName = "Boximon Damaged", menuName = "State Machine/Enemy/Boximon/State/Damaged")]
    public class BoximonDamaged : BoximonMachineData
    {
        [SerializeField, Foldout("Damaged")] private float knockBackTime = 0.5f;
        [SerializeField, Foldout("Damaged")] private float knockbackPower = 5f;
        public float KnockbackTime => knockBackTime;
        public float KnockbackPower => knockbackPower;
        public override BoximonMachineFunctions Initialize(BoximonMonoStateMachine machine)
        {
            return new BoximonDamagedFunctions(machine, this);
        }
    }
    public class BoximonDamagedFunctions : BoximonMachineFunctions
    {
        public float KnockbackTime;
        public float KnockbackPower;
        public BoximonDamagedFunctions(BoximonMonoStateMachine machine, BoximonDamaged data) : base(machine, data)
        {
            KnockbackTime = data.KnockbackTime;
            KnockbackPower = data.KnockbackPower;
            machine.Agent.isStopped = true;
            machine.StartCoroutine(Timer());
            if(data.IsKnockbackable && machine.CurrentTarget != null) machine.StartCoroutine(Knockback());
            
        }

        IEnumerator Timer()
        {
            yield return new WaitForSeconds(KnockbackTime);
            machine.OnEndState?.Invoke();
        }
        IEnumerator Knockback()
        {
            float TimeElapsed = 0;
            Vector3 KnockbackDir = machine.transform.position - machine.CurrentTarget.position;

            while (TimeElapsed < KnockbackTime)
            {
                TimeElapsed += Time.deltaTime;
                machine.transform.Translate(KnockbackDir.normalized * KnockbackPower * Time.deltaTime, Space.World);
                yield return null;
            }
            yield break;
        }

    }
}