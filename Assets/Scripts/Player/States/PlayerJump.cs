using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Player.State
{
    [CreateAssetMenu(fileName = "Player Jump", menuName = "State Machine/Player/State/Jump")]
    public class PlayerJump : PlayerMachineData
    {
        [SerializeField, Foldout("Jump")] private float jumpPower;

        public float JumpPower => jumpPower;
        public override PlayerMachineFunctions Initialize(PlayerMonoStateMachine machine)
        {
            return new PlayerJumpFunctions(machine, this);
        }
    }
    public class PlayerJumpFunctions : PlayerMachineFunctions
    {
        float JumpPower;
        public PlayerJumpFunctions(PlayerMonoStateMachine machine, PlayerJump data) : base(machine, data)
        {
            JumpPower = data.JumpPower;
            machine.PlayerRb.velocity += Vector3.up * JumpPower;
        }

    }
}