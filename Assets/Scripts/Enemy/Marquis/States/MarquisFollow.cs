using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
namespace StateMachine.Enemy.State
{
    [CreateAssetMenu(fileName = "Marquis Follow", menuName = "State Machine/Enemy/Marquis/State/Follow")]
    public class MarquisFollow : MarquisMachineData
    {
        [SerializeField, Foldout("Follow")] private float speed = 3.5f;
        public float Speed => speed;
        public override MarquisMachineFunctions Initialize(MarquisMonoStateMachine machine)
        {
            return new MarquisFollowFunctions(machine, this);
        }
    }

    public class MarquisFollowFunctions : MarquisMachineFunctions
    {
        float Speed;
        Transform target;
        public MarquisFollowFunctions(MarquisMonoStateMachine machine, MarquisFollow data) : base(machine, data)
        {
            target = GameObject.Find("Player").transform;
            Speed = data.Speed;
            machine.Agent.isStopped = false;
            machine.Agent.speed = Speed;
            machine.CurrentTarget = target;

        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            machine.Agent.destination = target.position;

        }
    }
}
