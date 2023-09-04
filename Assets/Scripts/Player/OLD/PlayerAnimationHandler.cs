using UnityEngine;
using Player.Movement;
using Player.Combat;
namespace Player.Animation
{
    public class PlayerAnimationHandler : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] PlayerReferences References;
        [Header("-----  Script Variables    -----")]
        public Animator PlayerAnimator;
        float WalkRunBlendSpeed = 0.5f;
        float MoveBlend;
        private void Update()
        {
            RunOrWalkSetter();
        }

        void RunOrWalkSetter()
        {
            MoveBlend = Mathf.Lerp(MoveBlend, References.PlayerMoveCS.MoveState == PlayerMoveState.Running ? 1 : 0, Time.deltaTime / WalkRunBlendSpeed);
            if (MoveBlend < 0.01f) MoveBlend = 0f;
            else if (MoveBlend > 0.95f && References.PlayerMoveCS.IsRunning) MoveBlend = 1f;
            PlayerAnimator.SetFloat("Move Blend", MoveBlend);
            PlayerAnimator.SetBool("IsIdle", References.PlayerMoveCS.IsIdle);
            PlayerAnimator.SetBool("IsRunning", References.PlayerMoveCS.IsRunning);
            PlayerAnimator.SetBool("IsWalking", References.PlayerMoveCS.IsWalking);
            PlayerAnimator.SetBool("IsAttacking", References.PlayerCombatCS.IsAttacking);
            PlayerAnimator.SetInteger("Attack Sequence", References.PlayerCombatCS.AttackSequence);

        }
    }
}

