using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine.Enemy.State
{
    [CreateAssetMenu(fileName = "Marquis Idle", menuName = "State Machine/Enemy/Marquis/State/Idle")]
    public class MarquisIdle : MarquisMachineData
    {
        public override MarquisMachineFunctions Initialize(MarquisMonoStateMachine machine)
        {
            return new MarquisIdleFunctions(machine, this);
        }

    }

    public class MarquisIdleFunctions : MarquisMachineFunctions
    {
        public MarquisIdleFunctions(MarquisMonoStateMachine machine, MarquisIdle data) : base(machine, data)
        {
            machine.Agent.isStopped = true;
        }
    }
}
