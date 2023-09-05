using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine.Enemy.State
{
    [CreateAssetMenu(fileName = "Boximon Follow", menuName = "State Machine/Enemy/Boximon/State/Follow")]
    public class BoximonFollow : BoximonMachineData
    {
        [SerializeField, Foldout("Follow")] private float speed = 3.5f;
        public float Speed => speed;
        public override BoximonMachineFunctions Initialize(BoximonMonoStateMachine machine)
        {
            return new BoximonFollowFunctions(machine, this);
        }
    }

    public class BoximonFollowFunctions : BoximonMachineFunctions
    {
        private Transform Target;
        private float Speed;
        public BoximonFollowFunctions(BoximonMonoStateMachine machine, BoximonFollow data) : base(machine, data)
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

