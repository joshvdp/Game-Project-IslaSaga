using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Player;
namespace Player.Controls
{
    public class PlayerInputs : MonoBehaviour
    {
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
            MoveInputs();
            AttackInputs();
            InteractionInputs();
        }

        private void AttackInputs()
        {
            if (Input.GetKeyDown(Controls.Attack1Key)) OnAttackOneInput?.Invoke();

            if (Input.GetKeyDown(Controls.Attack1Key) && machine.AttackSequence == 0) AttackOne?.Invoke();
            if (Input.GetKeyDown(Controls.Attack1Key) && machine.AttackSequence == 1) AttackTwo?.Invoke();
            if (Input.GetKeyDown(Controls.Attack1Key) && machine.AttackSequence == 2) AttackThree?.Invoke();
            if (Input.GetKeyDown(Controls.Attack1Key) && machine.AttackSequence == 3) SpinAttack?.Invoke();

        }
        private void MoveInputs()
        {
            if (Input.GetKey(Controls.ForwardKey)) OnMoveInput?.Invoke();
            if (Input.GetKey(Controls.BackwardKey)) OnMoveInput?.Invoke();
            if (Input.GetKey(Controls.LeftKey)) OnMoveInput?.Invoke();
            if (Input.GetKey(Controls.RightKey)) OnMoveInput?.Invoke();
            if (Input.GetKey(Controls.SprintKey)) OnSprintInput?.Invoke();
            else if (!Input.GetKey(Controls.SprintKey)) OnNoSprintInput?.Invoke();
        }

        private void InteractionInputs()
        {
            if (Input.GetKeyDown(Controls.PickUpKey)) OnPickupInput?.Invoke();
        }
    }
}

