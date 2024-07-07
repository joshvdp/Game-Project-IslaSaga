using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Player;
using Manager;
using UnityEngine.EventSystems;

namespace Player.Controls
{
    public enum PlatformType
    {
        PC,
        Mobile
    }
    public class PlayerInputs : MonoBehaviour
    {
        PlatformType PreviousPlatformType;


        public Action OnMoveInput;
        public Action OnSprintInput;
        public Action OnNoSprintInput;

        public Action OnJumpInput;
        public Action OnNoJumpInput;

        public Action OnAttackOneInput;

        public Action OnShieldInput;
        public Action OnNoShieldInput;

        public Action OnInteractInput;

        public Action AttackOne;
        public Action AttackTwo;
        public Action AttackThree;
        public Action SpinAttack;

        public Action OnPickupInput;

        public Action OnUseHPPotion;

        PlayerMonoStateMachine machine => GetComponent<PlayerMonoStateMachine>();

        public ControlBindings Controls;

        [SerializeField] bool ShowClickcastObjects;

        private void Start()
        {
            PreviousPlatformType = MainManager.Instance.Settings.PlatformType;
        }
        private void Update()
        {
            CheckIfChangedPlatformType();
            ListenToInputs();
        }

        void ListenToInputs()
        {
            switch (MainManager.Instance.Settings.PlatformType)
            {
                case PlatformType.PC:
                    PCMoveInputs();
                    PCAttackInputs();
                    PCInteractionInputs();
                    break;
                case PlatformType.Mobile:
                    MobileMoveInputs();
                    break;
            }
        }
        void CheckIfChangedPlatformType()
        {
            if(MainManager.Instance.Settings.PlatformType != PreviousPlatformType)
            {
                Debug.Log("CHANGED PLATFORM TYPE because previous platformtype is " + PreviousPlatformType + " and current platformtype now is " + MainManager.Instance.Settings.PlatformType);
                if (MainManager.Instance.Settings.PlatformType == PlatformType.PC) GlobalEvents.Instance.CallEvent("On Change Platform Type PC", this.GetType().ToString());
                if (MainManager.Instance.Settings.PlatformType == PlatformType.Mobile) GlobalEvents.Instance.CallEvent("On Change Platform Type Mobile", this.GetType().ToString());
                PreviousPlatformType = MainManager.Instance.Settings.PlatformType;
            }
        }
        #region PC Inputs
        void PCMoveInputs()
        {
            if (Input.GetKey(Controls.ForwardKey)) OnMoveInput?.Invoke();
            if (Input.GetKey(Controls.BackwardKey)) OnMoveInput?.Invoke();
            if (Input.GetKey(Controls.LeftKey)) OnMoveInput?.Invoke();
            if (Input.GetKey(Controls.RightKey)) OnMoveInput?.Invoke();
            if (Input.GetKey(Controls.SprintKey)) OnSprintInput?.Invoke();
            else if (!Input.GetKey(Controls.SprintKey)) OnNoSprintInput?.Invoke();

            if (Input.GetKeyDown(Controls.JumpKey)) OnJumpInput?.Invoke();
            if (Input.GetKeyDown(Controls.InventoryKey)) UIManager.Instance.ToggleScreen("Inventory");


        }
        private void PCAttackInputs()
        {
            if (Input.GetKeyDown(Controls.Attack1Key) && ShowClickcastObjects)
            {
                GetClickcastObjects(); // FOR DEBUGGING.
                Debug.Log(IsMouseOverUI());
            }
            if (IsMouseOverUI()) return;
            if (Input.GetKeyDown(Controls.Attack1Key))
            {
                OnAttackOneInput?.Invoke();
            }
            if (Input.GetKeyDown(Controls.Attack1Key) && machine.AttackSequence == 0) AttackOne?.Invoke();
            if (Input.GetKeyDown(Controls.Attack1Key) && machine.AttackSequence == 1) AttackTwo?.Invoke();
            if (Input.GetKeyDown(Controls.Attack1Key) && machine.AttackSequence == 2) AttackThree?.Invoke();
            if (Input.GetKeyDown(Controls.Attack1Key) && machine.AttackSequence == 3) SpinAttack?.Invoke();

            if (Input.GetKeyDown(Controls.ShieldKey)) OnShieldInput?.Invoke();
            if (Input.GetKeyUp(Controls.ShieldKey)) OnNoShieldInput?.Invoke();

            if (Input.GetKeyDown(Controls.QuickUsePotionKey)) OnUseHPPotion?.Invoke();

        }
        private void PCInteractionInputs()
        {
            if (Input.GetKeyDown(Controls.PickUpKey)) OnPickupInput?.Invoke();
            if (Input.GetKeyDown(Controls.InteractKey)) OnInteractInput?.Invoke();
        }
        #endregion
        #region Mouse Data
        bool IsMouseOverUI()
        {
            if (EventSystem.current) return EventSystem.current.IsPointerOverGameObject();
            else return false;
        }

        void GetClickcastObjects()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            for (int i = 0; i < results.Count; i++)
            {
                Debug.Log(results[i].gameObject.name);
            }
        }
        #endregion
        #region Mobile Inputs
        void MobileMoveInputs()
        {
            if (machine.MobileJoystick != null)
            {
                if (machine.MobileJoystick.Horizontal != 0 || machine.MobileJoystick.Vertical != 0)
                {
                    OnMoveInput?.Invoke();
                }
            }
            
        }
        public void MobileAttackInput()
        {
            OnAttackOneInput?.Invoke();
            if (machine.AttackSequence == 0) AttackOne?.Invoke();
            if (machine.AttackSequence == 1) AttackTwo?.Invoke();
            if (machine.AttackSequence == 2) AttackThree?.Invoke();
            if (machine.AttackSequence == 3) SpinAttack?.Invoke();
        }
        
        #endregion

    }
}

