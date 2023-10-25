using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Controls;
using StateMachine.Player;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Mobile
{
    public class MobileUIInputHandler : MonoBehaviour
    {
        
        public FixedJoystick Joystick;
        public FixedTouchField TouchField;

        private PlayerInputs _playerInput;
        public PlayerInputs PlayerInput => _playerInput ? _playerInput : FindObjectOfType<PlayerInputs>();

        private void Awake()
        {
            FindObjectOfType<PlayerMonoStateMachine>().MobileJoystick = Joystick;
            FindObjectOfType<CameraControllerNEW>().TouchField = TouchField;
        }

        public void InvokeAttack()
        {
            if(PlayerInput.PlatformType == PlatformType.Mobile) PlayerInput.MobileAttackInput();
        }

        public void InvokePickUp()
        {
            if (PlayerInput.PlatformType == PlatformType.Mobile) PlayerInput.OnPickupInput?.Invoke();
        }

        public void InvokeSprint()
        {
            if (PlayerInput.PlatformType == PlatformType.Mobile) PlayerInput.OnSprintInput?.Invoke();
        }
        public void InvokeNoSprint()
        {
            if (PlayerInput.PlatformType == PlatformType.Mobile) PlayerInput.OnNoSprintInput?.Invoke();
        }

        public void InvokeBlock()
        {
            if (PlayerInput.PlatformType == PlatformType.Mobile) PlayerInput.OnShieldInput?.Invoke();
        }
        public void InvokeNoBlock()
        {
            if (PlayerInput.PlatformType == PlatformType.Mobile) PlayerInput.OnNoShieldInput?.Invoke();
        }

        public void InvokeJump()
        {
            if (PlayerInput.PlatformType == PlatformType.Mobile) PlayerInput.OnJumpInput?.Invoke();
        }

    }
}

