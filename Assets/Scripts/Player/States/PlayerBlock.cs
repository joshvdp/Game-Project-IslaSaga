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

            ToggleShield(true);
            machine.StopMovement();
        }

        public override void Discard()
        {
            base.Discard();
            ToggleShield(false);
        }

        void ToggleShield(bool IsActive)
        {
            if (machine.ShieldCollider != null) machine.ShieldCollider.enabled = IsActive; 
        }

        
    }

}