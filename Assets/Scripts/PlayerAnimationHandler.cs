using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Movement;
namespace Player.Animation
{
    public class PlayerAnimationHandler : MonoBehaviour
    {
        [SerializeField] Animator PlayerAnimator;
        [SerializeField] PlayerMovement PlayerMoveCS;
        float WalkRunBlendSpeed = 0.5f;
        float MoveBlend;
        private void Update()
        {
            RunOrWalkSetter();
        }

        void RunOrWalkSetter()
        {
            MoveBlend = Mathf.Lerp(MoveBlend, PlayerMoveCS.MoveState == PlayerMoveState.Running ? 1 : 0, Time.deltaTime / WalkRunBlendSpeed);
            if (MoveBlend < 0.01f) MoveBlend = 0f;
            else if (MoveBlend > 0.95f && PlayerMoveCS.IsRunning) MoveBlend = 1f;

            PlayerAnimator.SetFloat("Move Blend", MoveBlend);
            PlayerAnimator.SetBool("IsIdle", PlayerMoveCS.IsIdle);
            PlayerAnimator.SetBool("IsRunning", PlayerMoveCS.IsRunning);
            PlayerAnimator.SetBool("IsWalking", PlayerMoveCS.IsWalking);
        }
    }
}

