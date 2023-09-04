using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Player.State
{
    [CreateAssetMenu(fileName = "Player Move", menuName = "State Machine/Player/State/Move")]
    public class PlayerMove : PlayerMachineData
    {
        [SerializeField, Foldout("Move")] private float speed;

        public float Speed => speed;
        public override PlayerMachineFunctions Initialize(PlayerMonoStateMachine machine)
        {
            return new PlayerMoveFunctions(machine, this);
        }
    }
    public class PlayerMoveFunctions : PlayerMachineFunctions
    {
        float Speed;
        public PlayerMoveFunctions(PlayerMonoStateMachine machine, PlayerMove data) : base(machine, data)
        {
            Speed = data.Speed;
        }

        public override void StateFixedUpdate()
        {
            base.StateFixedUpdate();
            machine.PlayerRb.velocity = machine.MoveVelocityInputs * Speed;
            machine.RotateTowardsMovement(300f, false);
        }

    }
}