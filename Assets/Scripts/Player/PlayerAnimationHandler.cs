using UnityEngine;
using Player.Movement;
using Player.Combat;
namespace Player.Animation
{
    public class PlayerAnimationHandler : MonoBehaviour
    {
        [Header("References")]
        public Animator PlayerAnimator;
        [SerializeField] PlayerMovement PlayerMoveCS;
        [SerializeField] PlayerCombat PlayerCombatCS;
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
            PlayerAnimator.SetBool("IsAttacking", PlayerCombatCS.IsAttacking);
            PlayerAnimator.SetInteger("Attack Sequence", PlayerCombatCS.AttackSequence);

        }
    }
}

