using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine.Enemy.State
{
    [CreateAssetMenu(fileName = "Marquis Stun", menuName = "State Machine/Enemy/Marquis/State/Stun")]
    public class MarquisStun : MarquisMachineData
    {
        public override MarquisMachineFunctions Initialize(MarquisMonoStateMachine machine)
        {
            return new MarquisStunFunctions(machine, this);
        }

    }

    public class MarquisStunFunctions : MarquisMachineFunctions
    {
        public MarquisStunFunctions(MarquisMonoStateMachine machine, MarquisStun data) : base(machine, data)
        {
            machine.Agent.isStopped = true;
            machine.StartCoroutine(StartStunTimer(machine.StunDuration));
        }

        IEnumerator StartStunTimer(float duration)
        {
            yield return new WaitForSeconds(duration);
            machine.OnStunEnd?.Invoke();
        }
    }

}
