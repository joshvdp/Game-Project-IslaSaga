using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Controls;
namespace StateMachine.Player.State
{
    [CreateAssetMenu(fileName = "Player Block", menuName = "State Machine/Player/State/Block")]
    public class PlayerBlock : PlayerMachineData
    {
        public override PlayerMachineFunctions Initialize(PlayerMonoStateMachine machine)
        {
            return new PlayerBlockFunctions(machine, this);
        }
    }

    public class PlayerBlockFunctions : PlayerMachineFunctions
    {
        public PlayerBlockFunctions (PlayerMonoStateMachine machine, PlayerBlock data) : base(machine, data)
        {
            if (machine.PlayerInputs.PlatformType == PlatformType.Mobile) machine.FaceToNearestEnemy();

            machine.ShieldCollider.enabled = true;
            machine.StopMovement();
        }

        public override void Discard()
        {
            base.Discard();
            machine.ShieldCollider.enabled = false;
        }

        
    }

}