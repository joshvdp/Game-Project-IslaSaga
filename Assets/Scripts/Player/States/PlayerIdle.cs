using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine.Player.State
{
    [CreateAssetMenu(fileName = "Player Idle", menuName = "State Machine/Player/State/Idle")]
    public class PlayerIdle : PlayerMachineData
    {
        public override PlayerMachineFunctions Initialize(PlayerMonoStateMachine machine)
        {
            return new PlayerIdleFunctions(machine, this);
        }
    }
    public class PlayerIdleFunctions : PlayerMachineFunctions
    {
        public PlayerIdleFunctions(PlayerMonoStateMachine machine, PlayerIdle data) : base(machine, data)
        {
            machine.PlayerRb.velocity = new Vector3(0, machine.PlayerRb.velocity.y, 0);
        }

    }
}