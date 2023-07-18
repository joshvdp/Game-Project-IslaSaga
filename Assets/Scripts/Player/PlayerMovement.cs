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
        public Vector3 CamRelativeMoveVect;
        Vector3 PlayerHeightPos;
        float HorizontalMove;
        float VerticalMove;

        [Header("Referencing")]
        [SerializeField] PlayerReferences References;

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

            PlayerMoveSpeed = References.PlayerStatsCS.PlayerSpeed;
        }


        float GetRunSpeed(float moveSpeed)
        {
            return moveSpeed *= 1.4f;
        }

        private void Update()
        {
            if(!References.PlayerCombatCS.IsAttacking && References.PlayerHpCS.IsAlive)
            {
                StateSetter();
                RotateTowardsMovement();
            }
        }
        



        private void FixedUpdate()
        {
            if (!References.PlayerCombatCS.IsAttacking && References.PlayerHpCS.IsAlive)
            {
                Move();
                SlopeHandler();
            }
        }
        

        void SimulateGravity()
        {
            References.PlayerRb.velocity += -Vector3.up * Gravity;
        }
        void Move()
        {
            HorizontalMove = Mathf.Clamp(HorizontalMove, -1, 1);
            VerticalMove = Mathf.Clamp(VerticalMove, -1, 1);

            Vector3 camForward = References.PlayerCamTransform.forward;
            Vector3 camRight = References.PlayerCamTransform.right;

            camForward.y = 0;
            camRight.y = 0;

            Vector3 forwardRelativeMoveVect = HorizontalMove * camForward;
            Vector3 rightRelativeMoveVect = VerticalMove * camRight;

            CamRelativeMoveVect = forwardRelativeMoveVect + rightRelativeMoveVect;

            References.PlayerRb.velocity = CamRelativeMoveVect.normalized * (IsRunning ? RunSpeed : PlayerMoveSpeed) * MoveValueHandler * Time.deltaTime;
            if (Input.GetKey(References.Controls.ForwardKey))
            {
                HorizontalMove +=  1;
            }
            if (Input.GetKey(References.Controls.BackwardKey))
            {
                HorizontalMove -= 1;
            }
            if (Input.GetKey(References.Controls.LeftKey))
            {
                VerticalMove -= 1;
            }
            if (Input.GetKey(References.Controls.RightKey))
            {
                VerticalMove += 1;
            }

            if(!Input.GetKey(References.Controls.ForwardKey) && !Input.GetKey(References.Controls.BackwardKey))
            {
                HorizontalMove = 0;
            }

            if(!Input.GetKey(References.Controls.RightKey) && !Input.GetKey(References.Controls.LeftKey))
            {
                VerticalMove = 0;
            }
            if (Input.GetKey(References.Controls.SprintKey) && References.PlayerRb.velocity.magnitude >= 0.1f)
            {
                IsRunning = true;
                IsWalking = false;
                IsIdle = false;
            }
            if (!Input.GetKey(References.Controls.SprintKey) || References.PlayerRb.velocity.magnitude <= 0f)
            {
                IsRunning = false;
                if(References.PlayerRb.velocity.magnitude >= 0.1f)
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
            if(References.PlayerCombatCS.IsAttacking)
            {
                MoveState = PlayerMoveState.Attacking;
                CamRelativeMoveVect = Vector3.zero;
            }
        }
        void RotateTowardsMovement()
        {
            Vector3 MoveDirection = new Vector3(CamRelativeMoveVect.x, 0f, CamRelativeMoveVect.z).normalized;
            Quaternion ForwardDirection = CamRelativeMoveVect.magnitude > 0.1f ? Quaternion.LookRotation(MoveDirection, Vector3.up):Quaternion.identity;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, ForwardDirection, 
                                RotationSpeed * Time.deltaTime * (CamRelativeMoveVect.magnitude != 0? 1 : 0));
        }

    }
}

