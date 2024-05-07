using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine.Enemy.State
{
    [CreateAssetMenu(fileName = "Boximon Idle", menuName = "State Machine/Enemy/Boximon/State/Idle")]
    public class BoximonIdle : BoximonMachineData
    {
        public override BoximonMachineFunctions Initialize(BoximonMonoStateMachine machine)
        {
            return new BoximonIdleFunctions(machine, this);
        }
    }

    public class BoximonIdleFunctions : BoximonMachineFunctions
    {
        public BoximonIdleFunctions(BoximonMonoStateMachine machine, BoximonIdle data) : base(machine, data)
        {
            machine.Agent.isStopped = true;
            machine.EnemySpawnPosition.StartWaitingForReturn(false);
        }
    }
}
