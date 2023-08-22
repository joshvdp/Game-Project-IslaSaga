using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine.Enemy.State
{
    [CreateAssetMenu(fileName = "Fiery Idle", menuName = "State Machine/Enemy/Fiery/State/Idle")]
    public class FieryIdle : FieryMachineData
    {

        public override FieryMachineFunctions Initialize(FieryMonoStateMachine machine)
        {
            return new FieryIdleFunctions(machine, this);
        }
    }

    public class FieryIdleFunctions : FieryMachineFunctions
    {
        public FieryIdleFunctions(FieryMonoStateMachine machine, FieryIdle data) : base(machine, data)
        {
            machine.Agent.velocity = Vector3.zero;
            
        }
    }
}
