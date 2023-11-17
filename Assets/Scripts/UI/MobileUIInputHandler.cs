using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Controls;
using StateMachine.Player;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Manager;
using NaughtyAttributes;
namespace Mobile
{
    public class MobileUIInputHandler : MonoBehaviour
    {
        
        public FixedJoystick Joystick;
        public FixedTouchField TouchField;

        private PlayerInputs _playerInput;
        public PlayerInputs PlayerInput => _playerInput ? _playerInput : FindObjectOfType<PlayerInputs>();

        [Foldout("Buttons")] public Button AttackButton;
        [Foldout("Buttons")] public Button SprintButton;
        [Foldout("Buttons")] public Button BlockButton;
        [Foldout("Buttons")] public Button InteractOrPickupButton;
        [Foldout("Buttons")] public Button JumpButton;
        [Foldout("Buttons")] public Button InventoryButton;

        PlayerMonoStateMachine PlayerMachine => FindObjectOfType<PlayerMonoStateMachine>();
        private void Awake()
        {
            PlayerMachine.MobileJoystick = Joystick;
            FindObjectOfType<CameraControllerNEW>().TouchField = TouchField;
        }

        private void Update()
        {
            CheckIfButtonsCanBePressed();
        }

        void CheckIfButtonsCanBePressed()
        {
            if (PlayerMachine.InteractableDetector.ObjectWithinDetectRange || PlayerMachine.PickUpRange.ObjectWithinDetectRange || PlayerMachine.PlayerIsHoldingObject) 
                InteractOrPickupButton.interactable = true;
            else InteractOrPickupButton.interactable = false;
        }

        public void InvokeAttack()
        {
            if(PlayerInput.PlatformType == PlatformType.Mobile) PlayerInput.MobileAttackInput();
        }

        public void InvokePickUpOrInteract()
        {
            if (PlayerInput.PlatformType == PlatformType.Mobile)
            {
                PlayerInput.OnInteractInput?.Invoke();
                PlayerInput.OnPickupInput?.Invoke();
            }
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

        public void InvokeInventory()
        {
            if (PlayerInput.PlatformType == PlatformType.Mobile) UIManager.Instance.ToggleScreen("Inventory");
        }

    }
}

