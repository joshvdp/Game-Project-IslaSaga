using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Controls;
using NaughtyAttributes;

namespace StateMachine.Player.State
{
    [CreateAssetMenu(fileName = "Player Block", menuName = "State Machine/Player/State/Block")]
    public class PlayerBlock : PlayerMachineData
    {
        [SerializeField, Foldout("Block")] float dragResist;

        public float DragResist => dragResist;
        public override PlayerMachineFunctions Initialize(PlayerMonoStateMachine machine)
        {
            return new PlayerBlockFunctions(machine, this);
        }
    }

    public class PlayerBlockFunctions : PlayerMachineFunctions
    {
        float DragResist;
        public PlayerBlockFunctions (PlayerMonoStateMachine machine, PlayerBlock data) : base(machine, data)
        {
            if (machine.PlayerInputs.PlatformType == PlatformType.Mobile) machine.FaceToNearestEnemy();
            DragResist = data.DragResist;
            machine.PlayerRb.drag = DragResist;
            ToggleShield(true);
            machine.StopMovement();
        }

        public override void Discard()
        {
            base.Discard();
            ToggleShield(false);
            machine.PlayerRb.drag = 0f;
        }

        void ToggleShield(bool IsActive)
        {
            if (machine.ShieldCollider != null) machine.ShieldCollider.enabled = IsActive;

            
            
        }

        
    }

}