using System.Collections;
using UnityEngine;
using Player.Movement;
using Manager;
namespace Player.Combat
{
    public class PlayerCombat : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] PlayerReferences References;
        [SerializeField] LayerMask RayHittableLayers;

        [Header("Attack Variables")]
        public bool IsAttacking;
        public int AttackSequence = 1;
        [SerializeField] float SequenceResetTime;
        private void Update()
        {
            if(References.PlayerHpCS.IsAlive) GetInput();

        }

        void GetInput()
        {
            if (Input.GetKeyDown(References.Controls.Attack1Key) && !IsAttacking && !MainManager.Instance.IsPaused)
            {
                FaceDirectionOfMousePos();
                Attack();
                soundUpdate();
            }
        }
        void Attack()
        {
            StopCoroutine("AttackSequenceReset");
            References.PlayerAttackRangeCS.UpdateList();
            ChangePlayerMovement(0f);
            References.PlayerPickUpCS.DropItem();
            References.PlayerMoveCS.IsRunning = false;
            References.PlayerMoveCS.IsWalking = false;
            References.PlayerMoveCS.IsIdle = false;
            IsAttacking = true;
            AttackSequence++;
            StartCoroutine("AttackSequenceReset");
        }

        void FaceDirectionOfMousePos()
        {
            Ray MouseRay = References.PlayerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(MouseRay, out RaycastHit rayCastHit, RayHittableLayers))
            {
                Vector3 DirectionToLookAt = new Vector3(rayCastHit.point.x, transform.position.y, rayCastHit.point.z);
                transform.rotation = Quaternion.LookRotation(DirectionToLookAt - transform.position, Vector3.up);
            }

        }
        public void CheckAttackSequence()
        {
            
            if (AttackSequence >= 4)
            {
                AttackSequence = 0;
                IsAttacking = false;
            }
            References.PlayerMoveCS.IsIdle = true;
            IsAttacking = false;
            ChangePlayerMovement(1f);
        }

        void ChangePlayerMovement(float value)
        {
            References.PlayerRb.velocity *= value;
            References.PlayerMoveCS.MoveValueHandler = value;
        }

        IEnumerator AttackSequenceReset()
        {
            yield return new WaitForSeconds(SequenceResetTime);
            IsAttacking = false;
            AttackSequence = 0;
        }
        public void soundUpdate ()
        {
            if (AttackSequence == 1)
            {
                WeaponSound.attackEvent1?.Invoke();
            }
            else if (AttackSequence == 2)
            {
                WeaponSound.attackEvent2?.Invoke();
            }
            else if (AttackSequence == 3)
            {
                WeaponSound.attackEvent3?.Invoke();
            }
            else if (AttackSequence == 4)
            {
                WeaponSound.attackEvent4?.Invoke();
            }
        }
           
    }
}
