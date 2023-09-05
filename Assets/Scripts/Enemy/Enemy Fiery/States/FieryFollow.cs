using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine.Enemy.State
{
    [CreateAssetMenu(fileName = "Fiery Follow", menuName = "State Machine/Enemy/Fiery/State/Follow")]
    public class FieryFollow : FieryMachineData
    {
        [SerializeField, Foldout("Follow")] private float speed = 3.5f;
        public float Speed => speed;
        public override FieryMachineFunctions Initialize(FieryMonoStateMachine machine)
        {
            return new FieryFollowFunctions(machine, this);
        }
    }

    public class FieryFollowFunctions : FieryMachineFunctions
    {
        private Transform Target;
        private float Speed;
        public FieryFollowFunctions(FieryMonoStateMachine machine, FieryFollow data) : base(machine, data)
        {
            if (machine.DetectCollider.NearestGameobject() != null) Target = machine.DetectCollider.NearestGameobject().transform;
            else machine.OnEndState?.Invoke();
            Speed = data.Speed;
            machine.Agent.isStopped = false;
            machine.Agent.speed = Speed;
            machine.CurrentTarget = Target;
        }
        public override void StateUpdate()
        {
            base.StateUpdate();
            if (Target == null)
            {
                machine.OnEndState?.Invoke();
                return;
            }
            machine.Agent.destination = Target.position;
        }
    }
}

