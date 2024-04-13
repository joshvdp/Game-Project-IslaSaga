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
        [Foldout("Buttons")] public Button QuickUseHpPotionButton;
        PlayerMonoStateMachine PlayerMachine;
        private void Awake()
        {
            PlayerMachine = FindObjectOfType<PlayerMonoStateMachine>();
            PlayerMachine.MobileJoystick = Joystick;
            FindObjectOfType<CameraControllerNEW>().TouchField = TouchField;
        }

        private void Update()
        {
            CheckIfButtonsCanBePressed();
        }

        void CheckIfButtonsCanBePressed()
        {
            if (PlayerMachine == null) return;
            if (PlayerMachine.InteractableDetector.ObjectWithinDetectRange || PlayerMachine.PickUpRange.ObjectWithinDetectRange || PlayerMachine.PlayerIsHoldingObject) 
                InteractOrPickupButton.interactable = true;
            else InteractOrPickupButton.interactable = false;

            QuickUseHpPotionButton.interactable = InventoryUIHandler.Instance.HasPotion;
        }

        #region Button Invokes
        public void InvokeAttack()
        {
            if(MainManager.Instance.Settings.PlatformType == PlatformType.Mobile) PlayerInput.MobileAttackInput();
        }

        public void InvokePickUpOrInteract()
        {
            if (MainManager.Instance.Settings.PlatformType == PlatformType.Mobile)
            {
                PlayerInput.OnInteractInput?.Invoke();
                PlayerInput.OnPickupInput?.Invoke();
            }
        }

        public void InvokeSprint()
        {
            if (MainManager.Instance.Settings.PlatformType == PlatformType.Mobile) PlayerInput.OnSprintInput?.Invoke();
        }
        public void InvokeNoSprint()
        {
            if (MainManager.Instance.Settings.PlatformType == PlatformType.Mobile) PlayerInput.OnNoSprintInput?.Invoke();
        }

        public void InvokeBlock()
        {
            if (MainManager.Instance.Settings.PlatformType == PlatformType.Mobile) PlayerInput.OnShieldInput?.Invoke();
        }
        public void InvokeNoBlock()
        {
            if (MainManager.Instance.Settings.PlatformType == PlatformType.Mobile) PlayerInput.OnNoShieldInput?.Invoke();
        }

        public void InvokeJump()
        {
            if (MainManager.Instance.Settings.PlatformType == PlatformType.Mobile) PlayerInput.OnJumpInput?.Invoke();
        }

        public void InvokeInventory()
        {
            if (MainManager.Instance.Settings.PlatformType == PlatformType.Mobile) UIManager.Instance.ToggleScreen("Inventory");
        }

        public void InvokeUseHpPotion()
        {
            if (MainManager.Instance.Settings.PlatformType == PlatformType.Mobile) PlayerInput.OnUseHPPotion?.Invoke();
        }
        #endregion
    }
}

