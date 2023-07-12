using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Combat;
namespace Player.Movement
{
    public enum PlayerMoveState
    {
        Walking,
        Running,
        Idle,
        Attacking
    }
    public class PlayerMovement : MonoBehaviour
    {
        [HideInInspector]
        public Vector3 MoveVector;
        Vector3 PlayerHeightPos;
        float HorizontalMove;
        float VerticalMove;

        [Header("Referencing")]
        [SerializeField] ControlBindings Controls;
        [SerializeField] public Rigidbody PlayerRb;
        [SerializeField] PlayerCombat PlayerCombatCS;


        [Header("Movement References/Variables")]
        [SerializeField] float Gravity;
        [SerializeField] Transform FeetRayStart;
        [SerializeField] LayerMask NavigatableAreas;
        [SerializeField] float HeightOffSet;

        [Header("Player Stats")]
        [SerializeField] float RotationSpeed;
        [SerializeField] float PlayerMoveSpeed;
        [SerializeField] float RunSpeed => GetRunSpeed(PlayerMoveSpeed);
        [HideInInspector] public float MoveValueHandler = 1f;

        [Header("Player State")]
        public PlayerMoveState MoveState;
        public bool IsRunning, IsWalking, IsIdle;

        private void Awake()
        {
            MoveValueHandler = 1f;
            IsIdle = true;
            MoveState = PlayerMoveState.Idle;
        }


        float GetRunSpeed(float moveSpeed)
        {
            return moveSpeed *= 1.4f;
        }

        private void Update()
        {
            if(!PlayerCombatCS.IsAttacking)
            {
                StateSetter();
                RotateTowardsMovement();
            }
        }

        

        private void FixedUpdate()
        {
            if (!PlayerCombatCS.IsAttacking)
            {
                Move();
                SlopeHandler();
            }

            
        }
        

        void SimulateGravity()
        {
            PlayerRb.velocity += -Vector3.up * Gravity;
        }
        void Move()
        {
            HorizontalMove = Mathf.Clamp(HorizontalMove, -1, 1);
            VerticalMove = Mathf.Clamp(VerticalMove, -1, 1);

            MoveVector = new Vector3(VerticalMove, 0f, HorizontalMove);
            PlayerRb.velocity = MoveVector.normalized * (IsRunning ? RunSpeed : PlayerMoveSpeed) * MoveValueHandler * Time.deltaTime;
            if (Input.GetKey(Controls.ForwardKey))
            {
                HorizontalMove +=  1;
            }
            if (Input.GetKey(Controls.BackwardKey))
            {
                HorizontalMove -= 1;
            }
            if (Input.GetKey(Controls.LeftKey))
            {
                VerticalMove -= 1;
            }
            if (Input.GetKey(Controls.RightKey))
            {
                VerticalMove += 1;
            }

            if(!Input.GetKey(Controls.ForwardKey) && !Input.GetKey(Controls.BackwardKey))
            {
                HorizontalMove = 0;
            }

            if(!Input.GetKey(Controls.RightKey) && !Input.GetKey(Controls.LeftKey))
            {
                VerticalMove = 0;
            }
            if (Input.GetKey(Controls.SprintKey) && PlayerRb.velocity.magnitude >= 0.1f)
            {
                IsRunning = true;
                IsWalking = false;
                IsIdle = false;
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

        void SlopeHandler()
        {
            if(Physics.Raycast(FeetRayStart.position, -Vector3.up,out RaycastHit hit, 0.6f, NavigatableAreas))
            {
                transform.position = new Vector3(transform.position.x, hit.point.y + HeightOffSet, transform.position.z);
            } else
            {
                SimulateGravity();
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
            if(PlayerCombatCS.IsAttacking)
            {
                MoveState = PlayerMoveState.Attacking;
                MoveVector = Vector3.zero;
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

