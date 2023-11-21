using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
namespace StateMachine.Enemy.State
{
    [CreateAssetMenu(fileName = "Marquis Choose Attack", menuName = "State Machine/Enemy/Marquis/State/Choose Attack")]
    public class MarquisChooseAttack : MarquisMachineData
    {
        [SerializeField, Foldout("Choose Attack")] private float chooseTime = 0.1f;

        public float ChooseTime => chooseTime;
        public override MarquisMachineFunctions Initialize(MarquisMonoStateMachine machine)
        {
            return new MarquisChooseAttackFunctions(machine, this);
        }
    }

    public class MarquisChooseAttackFunctions : MarquisMachineFunctions
    {
        public MarquisChooseAttackFunctions(MarquisMonoStateMachine machine, MarquisChooseAttack data) : base(machine, data)
        {
            machine.Agent.isStopped = true;
            machine.LookAtTarget();
            machine.Invoke("ChooseAttack", data.ChooseTime); // Not inovking like this causes stack error
        }

        
        

    }
}
