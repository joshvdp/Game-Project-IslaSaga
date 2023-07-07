using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Movement
{
    public enum PlayerMoveState
    {
        Walking,
        Running,
        Idle
    }
    public class PlayerMovement : MonoBehaviour
    {
        
        private PlayerControls input;
        Vector3 MoveVector;

        [Header("Referencing")]
        [SerializeField] ControlBindings Controls;
        [SerializeField] Rigidbody PlayerRb;

        [Header("Player Stats")]
        public PlayerMoveState MoveState;
        [SerializeField] float RotationSpeed;
        [SerializeField] float PlayerMoveSpeed;
        [SerializeField] float RunSpeed => GetRunSpeed(PlayerMoveSpeed);

        
        
        public bool IsRunning, IsWalking;
        public bool IsIdle;
        private void Awake()
        {
            input = new PlayerControls();
            IsIdle = true;
            MoveState = PlayerMoveState.Idle;
        }

        private void OnEnable()
        {
            input.Enable();
            input.Player.Movement.performed += OnMove;
        }
        private void OnDisable()
        {
            input.Disable();
            input.Player.Movement.performed -= OnMove;
        }
        void OnMove(InputAction.CallbackContext value)
        {
            MoveVector = value.ReadValue<Vector3>();
            MoveState = IsRunning? PlayerMoveState.Running : PlayerMoveState.Walking;
            IsWalking = (IsRunning && PlayerRb.velocity.magnitude >= 0.1f)? false: true;
            IsIdle = false;
        }

        float GetRunSpeed(float moveSpeed)
        {
            return moveSpeed *= 1.4f;
        }

        private void Update()
        {
            StateSetter();
            RotateTowardsMovement();
        }

        

        private void FixedUpdate()
        {
            Move();
        }

        void Move()
        {
            PlayerRb.velocity = MoveVector * (IsRunning ? RunSpeed : PlayerMoveSpeed) * Time.deltaTime;
            if (Input.GetKey(Controls.SprintKey) && PlayerRb.velocity.magnitude >= 0.1f)
            {
                IsRunning = true;
                IsWalking = false;
            }
            if (!Input.GetKey(Controls.SprintKey) || PlayerRb.velocity.magnitude <= 0f)
            {
                IsRunning = false;
                if(PlayerRb.velocity.magnitude >= 0.1f)
                {

                    IsWalking = true;
                    IsIdle = false;
                }
                else
                {

                    IsWalking = false;
                    IsIdle = true;
                }
            }
        }

        private void StateSetter()
        {
            if(IsRunning)
            {
                MoveState = PlayerMoveState.Running;
            }
            if (IsWalking)
            {
                MoveState = PlayerMoveState.Walking;
            }
            if (IsIdle)
            {
                MoveState = PlayerMoveState.Idle;
            }
        }
        void RotateTowardsMovement()
        {
            Vector3 MoveDirection = new Vector3(MoveVector.x, 0f, MoveVector.z).normalized;
            Quaternion ForwardDirection = MoveVector.magnitude > 0.1f ? Quaternion.LookRotation(MoveDirection, Vector3.up):Quaternion.identity;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, ForwardDirection, 
                                RotationSpeed * Time.deltaTime * (MoveVector.magnitude != 0? 1 : 0));
        }

    }
}

