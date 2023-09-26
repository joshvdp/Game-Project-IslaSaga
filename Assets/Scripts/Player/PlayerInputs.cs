using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Player;
namespace Player.Controls
{
    public enum PlatformType
    {
        PC,
        Mobile
    }
    public class PlayerInputs : MonoBehaviour
    {
        public PlatformType PlatformType;
        public Action OnMoveInput;
        public Action OnSprintInput;
        public Action OnNoSprintInput;

        public Action OnAttackOneInput;

        public Action AttackOne;
        public Action AttackTwo;
        public Action AttackThree;
        public Action SpinAttack;

        public Action OnPickupInput;
        PlayerMonoStateMachine machine => GetComponent<PlayerMonoStateMachine>();

        public ControlBindings Controls;

        
        private void Update()
        {
            ListenToInputs();
        }

        void ListenToInputs()
        {
            switch (PlatformType)
            {
                case PlatformType.PC:
                    PCMoveInputs();
                    PCAttackInputs();
                    PCInteractionInputs();
                    break;
                case PlatformType.Mobile:
                    MobileMoveInputs();
                    MobileAttackInputs();
                    MobileInteractionInputs();
                    break;
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
        }
        private void PCAttackInputs()
        {
            if (Input.GetKeyDown(Controls.Attack1Key)) OnAttackOneInput?.Invoke();
            if (Input.GetKeyDown(Controls.Attack1Key) && machine.AttackSequence == 0) AttackOne?.Invoke();
            if (Input.GetKeyDown(Controls.Attack1Key) && machine.AttackSequence == 1) AttackTwo?.Invoke();
            if (Input.GetKeyDown(Controls.Attack1Key) && machine.AttackSequence == 2) AttackThree?.Invoke();
            if (Input.GetKeyDown(Controls.Attack1Key) && machine.AttackSequence == 3) SpinAttack?.Invoke();

        }
        private void PCInteractionInputs()
        {
            if (Input.GetKeyDown(Controls.PickUpKey)) OnPickupInput?.Invoke();
        }
        #endregion

        #region Mobile Inputs
        void MobileMoveInputs()
        {
            if (machine.MobileJoystick.Horizontal != 0 || machine.MobileJoystick.Vertical != 0) OnMoveInput?.Invoke();
        }
        private void MobileAttackInputs()
        {

        }
        private void MobileInteractionInputs()
        {

        }
        #endregion

    }
}

